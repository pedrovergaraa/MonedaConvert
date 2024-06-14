using Microsoft.EntityFrameworkCore;
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
        public List<Currency> GetAllByUser(int id) 
        {
            return _context.Currencies.Include(c => c.User).Where(c => c.User.Id == id).Select(currency => new CurrencyDto()
            {
                Id = currency.Id,
                Legend = currency.Legend,
                Symbol = currency.Symbol,
                IC = currency.IC
            }).ToList();

        }

        //Boton de crear moneda
<<<<<<< HEAD
        public void CreateCurrency(CreateAndUpdateCurrencyDto dto, int currencyId)
=======
        public void CreateCurrency(CreateAndUpdateCurrencyDto dto, int loggedUserId)
>>>>>>> e2bdb655fa3a6cb462f9a5ff3f22a671aaf579f8
        {
            // Implementación del método
            Currency newCurrency = new Currency()
            {
                Symbol = dto.Symbol,
                Legend = dto.Legend,
                IC = dto.IC
            };
            _context.Currencies.Add(newCurrency);
            _context.SaveChanges();
        }
        //Lista de monedas para que el usuario pueda elegir
        //public List<UserCurrency> GetAllByUser(int userId)
        //{
        //    // Implementación del método
        //}


        public void RemoveCurrency(int currencyId)
        {
            _context.Currencies.Remove(_context.Currencies.Single(c => c.Id == currencyId));
            _context.SaveChanges();
            // Implementación del método
        }
        //Editar moneda
        public void UpdateCurrency(CreateAndUpdateCurrencyDto currencyDto, int coinId)
        {
            // Implementación del método
        }

        //void AddFavoriteCurrency(int userId, string currencyCode);
        //void RemoveFavoriteCurrency(int userId, string currencyCode);
        //decimal ConvertCurrency(int userId, string fromCurrencyCode, string toCurrencyCode, decimal amount);

    }
}