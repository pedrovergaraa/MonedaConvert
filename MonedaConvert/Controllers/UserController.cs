using Microsoft.AspNetCore.Mvc;
using CurrencyConvert.Services.Interfaces;
using CurrencyConvert.Models.Dtos;

namespace CurrencyConvert.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Endpoint para registrar un nuevo usuario
        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateAndUpdateUserDto dto)
        {
            bool isRegistered = _userService.RegisterUser(dto.Name, dto.Email, dto.Password);
            if (!isRegistered)
                return BadRequest("User with this email already exists");

            return Ok("User registered successfully");
        }

        // Endpoint para iniciar sesión (login)
        [HttpPost("login")]
        public IActionResult Login([FromBody] CreateAndUpdateUserDto dto)
        {
            var user = _userService.LoginUser(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized("Invalid email or password");

            return Ok("Login successful");
        }
    }
}
