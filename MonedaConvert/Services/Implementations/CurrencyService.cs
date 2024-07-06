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

        public float Convert(Currency FromCurrency, Currency ToCurrency, float amount, int userId)
        {
            User? user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            user.totalConversions = user.totalConversions + 1;
            long maxConvertions = user.Subscription.Convertions;

            if (user.totalConversions < maxConvertions)
            {
                if (FromCurrency.CurrencyId != ToCurrency.CurrencyId)
                {
                    float totalAmount = amount * FromCurrency.IC / ToCurrency.IC;
                    return totalAmount;
                }
                else
                {
                    return amount;
                }
            }

            return 0;
        }

        public void CreateCurrency(CreateAndUpdateCurrencyDto dto, int userId)

        {
            // Implementación del método
            Currency newCurrency = new Currency()
            {
                Symbol = dto.Symbol,
                Legend = dto.Legend,
                IC = dto.IC,
                UserId = userId,
            };
            _context.Currency.Add(newCurrency);
            _context.SaveChanges();
        }

        public Currency GetById(int currencyId)
        {
            return _context.Currency.SingleOrDefault(u => u.CurrencyId == currencyId);
        }

        public List<Currency> GetAllCurrencies(int userId)
        {
            return _context.Currency.Where(c => c.User.UserId == userId).ToList();
        }

        public bool CheckIfCurrencyExists(int currencyId)
        {
            Currency? currency = _context.Currency.FirstOrDefault(u => u.CurrencyId == currencyId);
            return currency != null;
        }

        public void RemoveCurrency(int currencyId)
        {
            _context.Currency.Remove(_context.Currencies.Single(c => c.Id == currencyId));
            _context.SaveChanges();
            // Implementación del método
        }
        //Editar moneda
        public void UpdateCurrency(CreateAndUpdateCurrencyDto dto, int currencyId)
        {
            Currency currencyToUpdate = _context.Currency.First(u => u.CurrencyId == currencyId);
            currencyToUpdate.Legend = dto.Legend;
            currencyToUpdate.Symbol = dto.Symbol;
            currencyToUpdate.IC = dto.IC;
            _context.SaveChanges();
        }
    }
}