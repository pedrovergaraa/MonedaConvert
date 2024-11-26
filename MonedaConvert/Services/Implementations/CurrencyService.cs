using CurrencyConvert.Data;
using CurrencyConvert.Entities;
using CurrencyConvert.Models.Dtos;
using Microsoft.EntityFrameworkCore;


namespace CurrencyConvert.Services.Implementations
{

    public class CurrencyService
    {
        private readonly CurrencyContext _context;

        public CurrencyService(CurrencyContext context)
        {
            _context = context;
        }

        public List<Currency> GetAllCurrencies()
        {
            return _context.Currencies.ToList();
        }

        public List<Currency> GetDefaultCurrencies()
        {
                return _context.Currencies
        .Where(c => c.UserId == 0 || c.UserId == null)
        .ToList();


        }
        public List<CurrencyDto> GetUserCurrencies(int userId)
        {
            var currencies = _context.Currencies
                .Where(c => c.UserId == userId || c.IsDefault)  
                .Select(c => new CurrencyDto
                {
                    CurrencyId = c.CurrencyId,
                    Legend = c.Legend,
                    Symbol = c.Symbol,
                    IC = c.IC,
                    IsFavorite = _context.FavoriteCurrencies.Any(fc => fc.CurrencyId == c.CurrencyId && fc.UserId == userId)
                }).ToList();

            return currencies;
        }



        public Currency GetCurrencyById(int currencyId)
        {
            return _context.Currencies.Find(currencyId);
        }

        public float ConvertCurrency(int userId, int fromCurrencyId, int toCurrencyId, float amount)
        {
            var user = _context.Users.Include(u => u.Subscription).FirstOrDefault(u => u.UserId == userId);
            if (user == null || user.Subscription == null)
            {
                throw new Exception("User or subscription not found.");
            }

            if (user.Attempts <= 0)
            {
                throw new Exception("You have no remaining conversion attempts.");
            }

            // Validar acceso a las monedas
            var fromCurrency = _context.Currencies
                .FirstOrDefault(c => c.CurrencyId == fromCurrencyId && (c.UserId == userId || c.UserId == null));
            var toCurrency = _context.Currencies
                .FirstOrDefault(c => c.CurrencyId == toCurrencyId && (c.UserId == userId || c.UserId == null));

            if (fromCurrency == null || toCurrency == null)
            {
                throw new Exception("One or both currencies not found or not accessible to the user.");
            }

            if (fromCurrency.IC <= 0 || toCurrency.IC <= 0)
            {
                throw new Exception("Invalid conversion rate.");
            }

            var conversionRate = amount * fromCurrency.IC / toCurrency.IC;

            user.Attempts -= 1;
            _context.SaveChanges();

            return conversionRate;
        }



        public void CreateCurrency(CreateAndUpdateCurrencyDto dto)
        {
            var currency = new Currency
            {
                Symbol = dto.Symbol,
                Legend = dto.Legend,
                IC = dto.IC,
                UserId = dto.userId,
                IsDefault = false
            };

            _context.Currencies.Add(currency);
            _context.SaveChanges();
        }

        public void UpdateCurrency(int currencyId, CreateAndUpdateCurrencyDto dto)
        {
            var currency = _context.Currencies.Find(currencyId);

            if (currency == null)
                throw new Exception("Currency not found.");

            currency.Legend = dto.Legend;
            currency.Symbol = dto.Symbol;
            currency.IC = dto.IC;

            _context.SaveChanges();
        }

        public void DeleteCurrency(int currencyId)
        {
            var currency = _context.Currencies.Find(currencyId);

            if (currency == null)
                throw new Exception("Currency not found.");

            _context.Currencies.Remove(currency);
            _context.SaveChanges();
        }


        public void MarkCurrencyAsFavorite(MarkFavoriteDto dto, int userId)
        {
            var existingFavorite = _context.FavoriteCurrencies
                .FirstOrDefault(fc => fc.CurrencyId == dto.CurrencyId && fc.UserId == userId);

            if (existingFavorite == null)
            {
                var favorite = new FavoriteCurrency
                {
                    CurrencyId = dto.CurrencyId,
                    UserId = userId
                };
                _context.FavoriteCurrencies.Add(favorite);
            }
            else
            {
                // Ya está marcado como favorito
                throw new Exception("Currency is already marked as favorite.");
            }

            _context.SaveChanges();
        }



        public void RemoveCurrencyFromFavorites(MarkFavoriteDto dto, int userId)
        {
            var favorite = _context.FavoriteCurrencies
                .FirstOrDefault(fc => fc.CurrencyId == dto.CurrencyId && fc.UserId == userId);

            if (favorite != null)
            {
                _context.FavoriteCurrencies.Remove(favorite);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Currency is not marked as favorite.");
            }
        }

        public List<CurrencyDto> GetUserCurrenciesWithFavorites(int userId)
        {
            // Trae solo las monedas favoritas para el usuario
            var favoriteCurrencyIds = _context.FavoriteCurrencies
                .Where(f => f.UserId == userId)
                .Select(f => f.CurrencyId)
                .ToList();

            var favoriteCurrencies = _context.Currencies
                .Where(c => favoriteCurrencyIds.Contains(c.CurrencyId)) // Filtra solo las monedas que son favoritas
                .ToList();

            // Convierte las monedas favoritas en CurrencyDto
            return favoriteCurrencies.Select(c => new CurrencyDto
            {
                CurrencyId = c.CurrencyId,
                Legend = c.Legend,
                Symbol = c.Symbol,
                IC = c.IC,
                IsFavorite = true // Ya son todas favoritas, no es necesario verificar si está en la lista
            }).ToList();
        }





    }
}
