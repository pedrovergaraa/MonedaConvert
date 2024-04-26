using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonedaConvert.Models.Dtos;
using MonedaConvert.Services.Interfaces;

namespace MonedaConvert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        //Se podra Crear, modificar, consultar y eliminar monedas en base de datos

        private readonly ICurrencyService _currencyService;
        private readonly IUserService _userService;

        public CurrencyController(ICurrencyService currencyService, IUserService userService)
        {
            _currencyService = currencyService;
            _userService = userService;

        }
        [HttpGet]
        public IActionResult GetAll() { }


        [HttpGet("{currencyId}")]
        public IActionResult GetById(int currencyId) 
        {
            
        }
    }
}
