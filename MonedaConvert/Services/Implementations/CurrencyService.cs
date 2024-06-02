//using MonedaConvert.Data;
//using MonedaConvert.Entities;
//using MonedaConvert.Models.Dtos;
//using MonedaConvert.Services.Interfaces;
//using System.Globalization;


namespace MonedaConvert.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly MonedaContext _context;

        public CurrencyService(MonedaContext context)
        {
            _context = context;
        }
        // Otros métodos de la clase
        // Otros métodos de la clase

    //    public void CreateCoin(CreateAndUpdateUserDto userDto, int currencyId)
    //    {
    //        // Implementación del método
    //    }

    //    public List<UserCurrency> GetAllByUser(int userId)
    //    {
    //        // Implementación del método
    //    }

    //    public Currency GetById(int userId, int currencyId)
    //    {
    //        // Implementación del método
    //    }

    //    public void RemoveCoin(int currencyId)
    //    {
    //        // Implementación del método
    //    }

    //    public void UpdateCoin(CreateAndUpdateCurrencyDto currencyDto, int coinId)
    //    {
    //        // Implementación del método
    //    }
    }
}