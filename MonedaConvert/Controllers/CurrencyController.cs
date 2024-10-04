﻿using ConversionDeMonedas.Data;
using ConversionDeMonedas.Entities;
using ConversionDeMonedas.Models.Dtos;
using ConversionDeMonedas.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConversionDeMonedas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyService _currencyService;
        private readonly ConversionDeMonedasContext _context;

        public CurrencyController(CurrencyService currencyService, ConversionDeMonedasContext context)
        {
            _currencyService = currencyService;
            _context = context;
        }

        [HttpGet("Convert")]
        public IActionResult Convert([FromQuery] double amount, [FromQuery] ToConvert toConvert)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
            User? user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user.TotalConversions != 0)
            {
                try
                {
                    double result = _currencyService.Convert(user, amount, toConvert);
                    user.TotalConversions -= 1;
                    _context.SaveChanges();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                double result = -1;
                return Ok(result);
            }
        }

        [HttpPost("CreateCurrency")]
        public IActionResult CreateCurrency(CreateAndUpdateCurrencyDto dto)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
            try
            {
                _currencyService.CreateCurrency(userId, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok("Created Successfully");
        }

        [HttpPut("EditCurrency")]
        public IActionResult UpdateCurrency(int currencyId, string legend, CreateAndUpdateCurrencyDto dto)
        {
            try
            {
                _currencyService.UpdateCurrency(currencyId, legend, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok("Edited Successfully");
        }

        [HttpDelete("DeleteCurrency")]
        public IActionResult DeleteCurrency(int currencyId, string legend)
        {
            try
            {
                _currencyService.DeleteCurrency(currencyId, legend);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok("Deleted Successfully");
        }

        [HttpPost("AddFavoriteCurrency")]
        public IActionResult AddFavoriteCurrency(AddFavoriteCurrencyDto dto)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);

            try
            {
                _currencyService.AddFavoriteCurrency(userId, dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok("Favorite currency added successfully.");
        }

        [HttpDelete("RemoveFavoriteCurrency")]
        public IActionResult RemoveFavoriteCurrency(int favoriteCurrencyId)
        {
            try
            {
                _currencyService.RemoveFavoriteCurrency(favoriteCurrencyId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok("Favorite currency removed successfully.");
        }
    }
}
}
