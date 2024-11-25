using Microsoft.AspNetCore.Mvc;
using CurrencyConvert.Services.Implementations;
using CurrencyConvert.Models.Dtos;
using CurrencyConvert.Entities;
using System;
using System.Linq;

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

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticationRequestDto dto)
        {
            try
            {
                var user = _userService.ValidateUser(dto);
                if (user == null)
                {
                    return Unauthorized("Correo o contraseña incorrectos");
                }

                return Ok(new
                {
                    Message = "Autenticación exitosa",
                    UserId = user.UserId,
                    Email = user.Email,
                    SubscriptionId = user.SubscriptionId ?? 1 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("register")]
        public IActionResult Register(CreateAndUpdateUserDto dto)
        {
            try
            {
                _userService.Create(dto);
                return Ok("Usuario creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
                return NotFound("Usuario no encontrado");

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Email = user.Email,
                SubscriptionId = user.SubscriptionId,
                Currencies = user.Currencies?.Select(c => new CurrencyDto
                {
                    CurrencyId = c.CurrencyId,
                    Legend = c.Legend,
                    Symbol = c.Symbol,
                    IC = c.IC
                }).ToList(),
                FavoriteCurrencies = user.FavoriteCurrencies?.Select(f => new MarkFavoriteDto
                {
                    CurrencyId = f.CurrencyId
                }).ToList()
            };

            return Ok(userDto);
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            if (!users.Any())
                return NotFound("No hay usuarios disponibles");

            var userDtos = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Email = u.Email,
                SubscriptionId = u.SubscriptionId,
                Currencies = u.Currencies?.Select(c => new CurrencyDto
                {
                    CurrencyId = c.CurrencyId,
                    Legend = c.Legend,
                    Symbol = c.Symbol,
                    IC = c.IC
                }).ToList(),
                FavoriteCurrencies = u.FavoriteCurrencies?.Select(f => new MarkFavoriteDto
                {
                    CurrencyId = f.CurrencyId
                }).ToList()
            }).ToList();

            return Ok(userDtos);
        }

        [HttpPut("{userId}")]
        public IActionResult UpdateUser(int userId, CreateAndUpdateUserDto dto)
        {
            try
            {
                _userService.UpdateUser(userId, dto);
                return Ok("Usuario actualizado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                _userService.DeleteUser(userId);
                return Ok("Usuario eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
