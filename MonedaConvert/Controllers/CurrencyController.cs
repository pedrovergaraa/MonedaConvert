using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CurrencyConvert.Data;
using CurrencyConvert.Entities;
using CurrencyConvert.Models.Dtos;
using CurrencyConvert.Services.Implementations;

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

        [HttpGet("GetAllCurrencies")]
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

        [HttpGet("GetFavoriteCurrencies")]
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

        [HttpGet("Convert")]
        public IActionResult Convert([FromQuery] float amount, [FromQuery] ConversionDto toConvert)
        {
            int userId = int.Parse(User.Claims.First(x => x.Type.Contains("nameidentifier"))!.Value);
            User? user = _context.Users.SingleOrDefault(u => u.UserId == userId);

            if (user is null)
                return Unauthorized("User not found.");

            if (user.TotalConversions == 0)
                return BadRequest("No conversions remaining.");

            try
            {
                float result = _currencyService.ConvertCurrency(user, amount, toConvert);
                user.TotalConversions -= 1;
                _context.SaveChanges();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Conversion error: {ex.Message}");
            }
        }

        [HttpPost("CreateCurrency")]
        public IActionResult CreateCurrency(CreateAndUpdateCurrencyDto dto)
        {
            int userId = int.Parse(User.Claims.First(x => x.Type.Contains("nameidentifier"))!.Value);

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
        [HttpPut("EditCurrency/{currencyId}")]
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

        [HttpDelete("DeleteCurrency/{currencyId}")]
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


        [HttpPost("AddFavoriteCurrency")]
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

        [HttpDelete("RemoveFavoriteCurrency")]
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
