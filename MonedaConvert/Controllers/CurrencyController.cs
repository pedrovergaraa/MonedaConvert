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
        [HttpGet("user")]
        public IActionResult GetUserCurrencies()
        {
            try
            {
                // Obtenemos el userId directamente del token
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));

                // Verificamos si se encuentra el userId en el claim
                if (userIdClaim == null)
                    return Unauthorized("No se pudo encontrar el ID de usuario en el token.");

                var userId = int.Parse(userIdClaim.Value);
                var userCurrencies = _currencyService.GetUserCurrencies(userId);
                return Ok(userCurrencies);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
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
        [HttpGet("favorites")]
        public IActionResult GetFavoriteCurrencies()
        {
            try
            {
                var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type.Contains("nameidentifier")).Value);
                var currencies = _currencyService.GetUserCurrenciesWithFavorites(userId);
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpGet("default")]
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

        [HttpPost("convert")]
        public IActionResult ConvertCurrency([FromBody] ConversionDto dto)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type.Contains("nameidentifier")).Value);
                var result = _currencyService.ConvertCurrency(userId, dto.FromCurrencyId, dto.ToCurrencyId, dto.Amount);
                return Ok(new { ConvertedAmount = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("remaining-conversions/{userId}")]
        public IActionResult GetRemainingConversions(int userId)
        {
            var user = _context.Users.Include(u => u.Subscription).FirstOrDefault(u => u.UserId == userId);
            if (user == null || user.Subscription == null)
            {
                return NotFound("User or subscription not found.");
            }

            return Ok(new
            {
                RemainingConversions = user.Attempts
            });
        }
        [HttpPost("create")]
        public IActionResult CreateCurrency([FromBody] CreateAndUpdateCurrencyDto dto)
        {
            try
            {
                // Verificar si el modelo es válido
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid data.");
                }

                _currencyService.CreateCurrency(dto);
                return Ok("Currency created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
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

        [HttpPost("addFavorite")]
        public IActionResult MarkFavorite([FromBody] MarkFavoriteDto dto)
        {
            try
            {
                // Obtiene el userId directamente desde los claims del token
                var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type.Contains("nameidentifier")).Value);
                _currencyService.MarkCurrencyAsFavorite(dto, userId);
                return Ok("Currency marked as favorite.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("favorites/{currencyId}")]
        public IActionResult RemoveFavorite(int currencyId) // Ya que el currencyId es un parámetro en la ruta, no lo necesitamos en el cuerpo
        {
            try
            {
                // Obtiene el userId directamente desde los claims del token
                var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type.Contains("nameidentifier")).Value);
                var dto = new MarkFavoriteDto { CurrencyId = currencyId }; // Crea el DTO para pasarlo al servicio
                _currencyService.RemoveCurrencyFromFavorites(dto, userId);
                return Ok("Currency removed from favorites.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
 }