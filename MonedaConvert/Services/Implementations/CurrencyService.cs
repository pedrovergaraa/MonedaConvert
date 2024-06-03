using MonedaConvert.Data;
using MonedaConvert.Entities;
using MonedaConvert.Models.Dtos;
using MonedaConvert.Services.Interfaces;
using System.Globalization;


namespace MonedaConvert.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly MonedaContext _context;

        public CurrencyService(MonedaContext context)
        {
            _context = context;
        }
        //Boton de crear moneda
        public void CreateCoin(CreateAndUpdateCurrencyDto dto, int currencyId)
        {
            // Implementación del método
            Currency newCurrency = new Currency()
            {
                Symbol = dto.Symbol,
                Legend = dto.Legend,
                IC = dto.IC
            };
            _context.Coins.Add(newCurrency);
            _context.SaveChanges();
        }
        //Lista de monedas para que el usuario pueda elegir
        //public List<UserCurrency> GetAllByUser(int userId)
        //{
        //    // Implementación del método
        //}


        public void RemoveCoin(int currencyId)
        {
            _context.UserCurrency.Remove(_context.UserCurrency.Single(c => c.Id == currencyId));
            _context.SaveChanges();
            // Implementación del método
        }
        //Editar moneda
        public void UpdateCoin(CreateAndUpdateCurrencyDto currencyDto, int coinId)
        {
            // Implementación del método
        }
    }
}