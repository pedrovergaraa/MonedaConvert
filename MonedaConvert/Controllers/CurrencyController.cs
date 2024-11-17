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
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            try
            {
                var favoriteCurrencies = _currencyService.GetFavoriteCurrencies(userId);
                return Ok(favoriteCurrencies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("default")]
        public IActionResult GetDefaultCurrencies()
        {
            try
            {
                // Lista hardcodeada de monedas predeterminadas
                var defaultCurrencies = new List<Currency>
        {
            new Currency { CurrencyId = 1, Legend = "USD", Symbol = "$", IC = 1.0f, UserId = 0 }, 
            new Currency { CurrencyId = 2, Legend = "EUR", Symbol = "€", IC = 0.9f, UserId = 0 },
            new Currency { CurrencyId = 3, Legend = "GBP", Symbol = "£", IC = 0.8f, UserId = 0 },
            new Currency { CurrencyId = 4, Legend = "JPY", Symbol = "¥", IC = 110.0f, UserId = 0 }
        };

                return Ok(defaultCurrencies);
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

                // Validar intentos restantes
                if (user.TotalConversions == 0)
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
                _currencyService.AddFavoriteCurrency(userId, dto);
                return Ok("Favorite currency added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding favorite currency: {ex.Message}");
            }
        }

        [HttpDelete("favorites/{favoriteCurrencyId}")]
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
