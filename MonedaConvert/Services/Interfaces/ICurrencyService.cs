using MonedaConvert.Entities;
using MonedaConvert.Models.Dtos;


namespace MonedaConvert.Services.Interfaces
{
    public interface ICurrencyService
    {
        //Le paso 2 parametros a los metodos porque cada usuario tiene sus propias monedas
        void CreateCurrency(CreateAndUpdateUserDto dto, int loggedUserId);

        List<CurrencyDto> GetAllByUser(int id);

        CurrencyDto GetById(int userId, int currencyId);

        void RemoveCurrency(int id);

        void UpdateCurrency(CreateAndUpdateCurrencyDto dto, int currencyId);

        bool CheckIfCurrencyExists(int currencyId);

        float Convert(Currency FromCurrency, Currency ToCurrency, float amount, int userId)
    }
}
