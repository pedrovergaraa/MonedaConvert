using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CurrencyConvert.Data;
using CurrencyConvert.Entities;
using CurrencyConvert.Models.Dtos;
using CurrencyConvert.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConvert.Controllers
{
    [Route("api/currency")]
    [ApiController]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyService _currencyService;
        private readonly CurrencyContext _context;

        public CurrencyController(CurrencyService currencyService, CurrencyContext context)
        {
            _currencyService = currencyService;
            _context = context;
        }

        [HttpGet("all")]
        public IActionResult GetAllCurrencies()
        {
            try
            {
                var currencies = _currencyService.GetAllCurrencies();
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{currencyId}")]
        public IActionResult GetCurrencyById(int currencyId)
        {
            try
            {
                // Obtiene la moneda usando el servicio
                var currency = _currencyService.GetCurrencyById(currencyId);

                // Si la moneda no existe, devuelve un error 404
                if (currency == null)
                {
                    return NotFound("Currency not found.");
                }

                return Ok(currency);  // Devuelve la moneda encontrada
            }
            catch (Exception ex)
            {
                // Devuelve un error si ocurre una excepción
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("favorites")]
        public IActionResult GetFavoriteCurrencies()
        {
            try
            {
                // Obtener el userId desde el token JWT
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

                // Obtener las monedas favoritas desde el servicio
                var favoriteCurrencies = _currencyService.GetFavoriteCurrencies(userId);

                // Si no se encuentran monedas favoritas, devolver un NotFound
                if (favoriteCurrencies == null || !favoriteCurrencies.Any())
                {
                    return NotFound("No favorite currencies found for the user.");
                }

                // Mapear la respuesta para enviar solo los datos necesarios
                var result = favoriteCurrencies.Select(fc => new
                {
                    fc.FavoriteCurrencyId,
                    fc.Legend,
                    fc.Symbol,
                    fc.IC
                });

                // Retornar las monedas favoritas
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }



        [HttpGet("defaultCurrencies")]
        public IActionResult GetDefaultCurrencies()
        {
            try
            {
                // Obtener las monedas predeterminadas llamando al servicio
                var defaultCurrencies = _currencyService.GetDefaultCurrencies(0); // El 0 es para un usuario genérico o predeterminado, ya que no se utiliza en este caso

                if (defaultCurrencies == null || !defaultCurrencies.Any())
                {
                    return NotFound("No default currencies found.");
                }

                // Mapear las monedas predeterminadas si es necesario
                var result = defaultCurrencies.Select(c => new
                {
                    c.CurrencyId,
                    c.Legend,
                    c.Symbol,
                    c.IC
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [HttpGet("convert")]
        public IActionResult Convert([FromQuery] float amount, [FromQuery] ConversionDto toConvert)
        {
            try
            {
                // Obtener el ID del usuario autenticado
                int userId = int.Parse(User.Claims.First(x => x.Type.Contains("nameidentifier"))!.Value);
                var user = _context.Users.Include(u => u.Subscription).SingleOrDefault(u => u.UserId == userId);

                if (user is null)
                    return Unauthorized("Usuario no encontrado.");

                // Si el usuario tiene el plan Free y no tiene intentos disponibles, establecer a 10 intentos
                if (user.SubscriptionId == 1 && user.TotalConversions == 0)
                {
                    user.TotalConversions = 10; // El plan Free tiene 10 conversiones disponibles
                    _context.SaveChanges();
                }

                // Validar intentos restantes
                if (user.TotalConversions <= 0)
                    return BadRequest("No tienes intentos restantes. Cambia de plan para obtener más.");

                // Realizar la conversión
                float result = _currencyService.ConvertCurrency(user, amount, toConvert);

                // Restar un intento y guardar cambios
                user.TotalConversions -= 1;
                _context.SaveChanges();

                return Ok(new
                {
                    Result = result,
                    RemainingConversions = user.TotalConversions
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error en la conversión: {ex.Message}");
            }
        }



        [HttpPost("create")]
        public IActionResult CreateCurrency(CreateAndUpdateCurrencyDto dto)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);

            try
            {
                _currencyService.CreateCurrency(userId, dto);
                return Ok("Currency created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating currency: {ex.Message}");
            }
        }


        [HttpPut("edit/{currencyId}")]
        public IActionResult UpdateCurrency(int currencyId, [FromBody] CreateAndUpdateCurrencyDto dto)
        {
            try
            {
                _currencyService.UpdateCurrency(currencyId, dto);  // Actualiza la moneda pasando el currencyId y el DTO
                return Ok("Currency edited successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error editing currency: {ex.Message}");
            }
        }

        [HttpDelete("{currencyId}")]
        public IActionResult DeleteCurrency(int currencyId)
        {
            try
            {
                _currencyService.DeleteCurrency(currencyId);  // Elimina la moneda pasando solo el currencyId
                return Ok("Currency deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting currency: {ex.Message}");
            }
        }

        [HttpPost("favorite")]
        public IActionResult AddFavoriteCurrency(AddFavoriteDto dto)
        {
            int userId = int.Parse(User.Claims.First(x => x.Type.Contains("nameidentifier"))!.Value);

            try
            {
                // Verificamos si la moneda ya está en favoritos y la agregamos si no lo está
                var existingFavorite = _context.FavoriteCurrencies
                    .FirstOrDefault(fc => fc.UserId == userId && fc.Legend == dto.Legend);

                if (existingFavorite != null)
                {
                    return BadRequest("This currency is already in your favorites.");
                }

                // Si la moneda no está en favoritos, la creamos y la agregamos
                FavoriteCurrency newFavorite = new FavoriteCurrency()
                {
                    Legend = dto.Legend,
                    Symbol = dto.Symbol,
                    IC = dto.IC,
                    UserId = userId
                };

                _context.FavoriteCurrencies.Add(newFavorite);
                _context.SaveChanges();

                return Ok("Favorite currency added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding favorite currency: {ex.Message}");
            }
        }

        [HttpDelete("favorite/{favoriteCurrencyId}")]
        public IActionResult RemoveFavorite(int favoriteCurrencyId)
        {
            try
            {
                _currencyService.RemoveFavoriteCurrency(favoriteCurrencyId);
                return Ok("Favorite currency removed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error removing favorite currency: {ex.Message}");
            }
        }
    }
}
