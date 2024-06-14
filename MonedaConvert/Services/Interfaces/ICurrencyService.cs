using MonedaConvert.Models.Dtos;


namespace MonedaConvert.Services.Interfaces
{
    public interface ICurrencyService
    {
        //Le paso 2 parametros a los metodos porque cada usuario tiene sus propias monedas
        void CreateCurrency(CreateAndUpdateUserDto dto, int loggedUserId);
        List<CurrencyDto> GetAllByUser(int id);
        CurrencyDto? GetById(int userId, int currencyId);
        void RemoveCurrency(int id);
        void UpdateCurrency(CreateAndUpdateCurrencyDto dto, int currencyId);
<<<<<<< HEAD
        void AddFavoriteCurrency(CurrencyDto, int currencyId);
=======
        void AddFavoriteCurrency(AddFavoriteCurrencyDto dto, int currencyId);
>>>>>>> e2bdb655fa3a6cb462f9a5ff3f22a671aaf579f8
    }
}
