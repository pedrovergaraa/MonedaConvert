using Microsoft.AspNetCore.Mvc;
using CurrencyConvert.Services.Implementations;
using CurrencyConvert.Models.Dtos;

namespace CurrencyConvert.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public IActionResult Create(CreateAndUpdateUserDto dto)
        {
            try
            {
                _userService.Create(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok("Creado correctamente");
        }
    }
}
