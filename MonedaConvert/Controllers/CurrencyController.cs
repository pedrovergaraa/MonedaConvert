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
                var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type.Contains("nameidentifier")).Value);
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

        [HttpPost("addFavorite")]
        public IActionResult AddToFavorites([FromBody] MarkFavoriteDto dto)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type.Contains("nameidentifier")).Value);
                _currencyService.AddToFavorites(userId, dto.CurrencyId);
                return Ok("Currency added to favorites.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("favorites/{currencyId}")]
        public IActionResult RemoveFromFavorites(int currencyId)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.Claims.First(c => c.Type.Contains("nameidentifier")).Value);
                _currencyService.RemoveFromFavorites(userId, currencyId);
                return Ok("Currency removed from favorites.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
