using MonedaConvert.Entities;
using MonedaConvert.Models.Dtos;

namespace MonedaConvert.Services.Interfaces
{
    public interface IUserService
    {
        User GetById(int id);
        void Create(CreateAndUpdateUserDto dto);
        bool ValidateUser(AuthenticationRequestDto authRequestBody);
        void DeleteUser(int userId);
        void AddFavoriteCurrency(int userId, string currencyCode);
        void RemoveFavoriteCurrency(int userId, string currencyCode);
        decimal ConvertCurrency(int userId, string fromCurrencyCode, string toCurrencyCode, decimal amount);
    }
}
