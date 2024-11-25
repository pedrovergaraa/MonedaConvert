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
            var userCurrencies = _context.Currencies
                .Where(c => c.UserId == userId || c.UserId == null) // Monedas del usuario y por defecto
                .Select(c => new CurrencyDto
                {
                    CurrencyId = c.CurrencyId,
                    Legend = c.Legend,
                    Symbol = c.Symbol,
                    IC = c.IC
                })
                .ToList();

            return userCurrencies;
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



        public void CreateCurrency(int userId, CreateAndUpdateCurrencyDto dto)
        {
            if (_context.Currencies.Any(c => c.Legend == dto.Legend && c.UserId == userId))
                throw new Exception("Currency already exists for this user.");

            _context.Currencies.Add(new Currency
            {
                Legend = dto.Legend,
                Symbol = dto.Symbol,
                IC = dto.IC,
                UserId = userId
            });

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


        public void AddToFavorites(int userId, int currencyId)
        {
            // Verificar que la moneda existe
            if (!_context.Currencies.Any(c => c.CurrencyId == currencyId))
            {
                throw new Exception("Currency not found.");
            }

            // Verificar que no esté ya marcada como favorita
            if (_context.FavoriteCurrencies.Any(f => f.UserId == userId && f.CurrencyId == currencyId))
            {
                throw new Exception("Currency already marked as favorite.");
            }

            var favoriteCurrency = new FavoriteCurrency
            {
                UserId = userId,
                CurrencyId = currencyId
            };

            _context.FavoriteCurrencies.Add(favoriteCurrency);
            _context.SaveChanges();
        }


        public void RemoveFromFavorites(int userId, int currencyId)
        {
            var favorite = _context.FavoriteCurrencies
                .FirstOrDefault(f => f.UserId == userId && f.CurrencyId == currencyId);

            if (favorite == null)
            {
                throw new Exception("Favorite currency not found.");
            }

            _context.FavoriteCurrencies.Remove(favorite);
            _context.SaveChanges();
        }


        public List<CurrencyDto> GetUserCurrenciesWithFavorites(int userId)
        {
            // Obtener el usuario junto con sus monedas
            var user = _context.Users
                .Include(u => u.Currencies) // Incluye las monedas asociadas
                .FirstOrDefault(u => u.UserId == userId);

            // Verifica si el usuario existe
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Obtener los IDs de las monedas favoritas del usuario
            var favoriteCurrencyIds = _context.FavoriteCurrencies
                .Where(f => f.UserId == userId)
                .Select(f => f.CurrencyId)
                .ToList();

            // Crear el DTO con las monedas y marcar las favoritas
            var currencyDtos = user.Currencies.Select(c => new CurrencyDto
            {
                CurrencyId = c.CurrencyId,
                Legend = c.Legend,
                Symbol = c.Symbol,
                IC = c.IC,
                IsFavorite = favoriteCurrencyIds.Contains(c.CurrencyId) // Marca como favorita si está en la lista
            }).ToList();

            // Agrega depuración aquí para verificar que los DTOs son correctos
            foreach (var currency in currencyDtos)
            {
                Console.WriteLine($"CurrencyId: {currency.CurrencyId}, IsFavorite: {currency.IsFavorite}");
            }

            return currencyDtos;
        }





    }
}
