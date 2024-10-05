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
        public IActionResult Authenticate([FromBody] AuthenticationRequestDto authenticationRequestBody)
        {
            // Paso 1: Validamos las credenciales del usuario
            var user = _userService.ValidateUser(authenticationRequestBody);

            if (user == null)
                return Unauthorized("Invalid email or password"); // Si no es válido, devolvemos Unauthorized

            // Paso 2: Crear el token JWT
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims: Se añaden datos del usuario que se incluirán en el JWT
            var claims = new List<Claim>
            {
                new Claim("sub", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim("role", "User"), // Agregar más información si es necesario
            };

            var jwtToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return Ok(new { Token = tokenString }); // Devolvemos el token JWT
        }
    }
}
