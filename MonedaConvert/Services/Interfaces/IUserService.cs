using CurrencyConvert.Entities;
using CurrencyConvert.Models.Dtos;

namespace CurrencyConvert.Services.Interfaces
{
    public interface IUserService
    {
        // Método para validar las credenciales del usuario
        User? ValidateUser(AuthenticationRequestDto authenticationRequest);
    }
}
