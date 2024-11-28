using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CurrencyConvert.Models.Dtos;
using CurrencyConvert.Services.Implementations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CurrencyConvert.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserService _userService;

        public AuthenticationController(IConfiguration config, UserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationRequestDto authenticationRequestBody)
        {
            try
            {
                var user = _userService.ValidateUser(authenticationRequestBody);
                if (user is null)
                    return Unauthorized();

                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:Key"]));
                var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                var claimsForToken = new List<Claim>
        {
            new Claim("sub", user.UserId.ToString()),
            new Claim("Email", user.Email)
        };

                var jwtSecurityToken = new JwtSecurityToken(
                    _config["Authentication:Issuer"],
                    _config["Authentication:Audience"],
                    claimsForToken,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials);

                var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                // Incluye el userId en la respuesta
                return Ok(new
                {
                    token = tokenToReturn,
                    userId = user.UserId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
    }
