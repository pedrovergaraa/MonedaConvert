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
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("{currencyId}")]
        public IActionResult GetCurrencyById(int currencyId)
        {
            try
            {
                var currency = _currencyService.GetCurrencyById(currencyId);
                if (currency == null)
                {
                    return NotFound("Currency not found.");
                }
                return Ok(currency);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("favorites/{userId}")]
        public IActionResult GetFavoriteCurrencies(int userId)
        {
            var favoriteCurrencies = _currencyService.GetFavoriteCurrencies(userId);
            if (favoriteCurrencies == null || !favoriteCurrencies.Any())
                return NotFound("No favorite currencies found.");

            return Ok(favoriteCurrencies);
        }

        [HttpGet("defaultCurrencies")]
        public IActionResult GetDefaultCurrencies()
        {
            try
            {
                var defaultCurrencies = _currencyService.GetDefaultCurrencies();

                if (defaultCurrencies == null)
                {
                    return NotFound("Error: Unable to fetch default currencies.");
                }

                if (!defaultCurrencies.Any())
                {
                    return Ok(new List<object>()); // Devuelve una lista vacía
                }


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
public IActionResult Convert([FromQuery] float amount, [FromQuery] int currencyFromId, [FromQuery] int currencyToId)
{
    try
    {
        int userId = int.Parse(User.Claims.First(x => x.Type.Contains("nameidentifier"))!.Value);
        User user = _context.Users.Include(u => u.Subscription).SingleOrDefault(u => u.UserId == userId);

        if (user == null)
            return Unauthorized("Usuario no encontrado.");

        if (amount <= 0)
            return BadRequest("La cantidad debe ser mayor que 0.");

        if (currencyFromId <= 0 || currencyToId <= 0)
            return BadRequest("Las tasas de conversión no pueden ser cero o negativas.");

        // Realizar la conversión
        float result = _currencyService.ConvertCurrency(user, amount, currencyFromId, currencyToId);

        // Enviar la respuesta con los resultados
        return Ok(new
        {
            Result = result,
            RemainingConversions = user.Subscription.AllowedAttempts == int.MaxValue
                ? "Unlimited"
                : (user.Subscription.AllowedAttempts - user.Attempts).ToString()
        });
    }
    catch (Exception ex)
    {
        if (ex.Message.Contains("No tienes intentos restantes"))
            return BadRequest(new
            {
                Error = ex.Message,
                Action = "Por favor, cambia a un plan superior para continuar usando el servicio."
            });

        return BadRequest($"Error en la conversión: {ex.Message}");
    }
}



        [HttpPost("create")]
        public IActionResult CreateCurrency([FromBody] CreateAndUpdateCurrencyDto dto)
        {
            try
            {
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
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
                _currencyService.UpdateCurrency(currencyId, dto);
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
                _currencyService.DeleteCurrency(currencyId);
                return Ok("Currency deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting currency: {ex.Message}");
            }
        }

        [HttpPost("favorite")]
        public IActionResult AddFavoriteCurrency([FromBody] AddFavoriteDto dto)
        {
            try
            {
                var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyId == dto.CurrencyId);
                if (currency == null)
                 throw new Exception("Currency not found.");

                int userId = int.Parse(User.Claims.First(x => x.Type.Contains("nameidentifier"))!.Value);
                _currencyService.AddFavoriteCurrency(userId, dto);
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
