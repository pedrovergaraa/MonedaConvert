using CurrencyConvert.Data;
using CurrencyConvert.Entities;
using CurrencyConvert.Models.Dtos;

namespace CurrencyConvert.Services.Implementations
{
    public class CurrencyService
    {
        private readonly CurrencyContext _context;

        public CurrencyService(CurrencyContext context)
        {
            _context = context;
        }

        // Obtener todas las monedas disponibles
        public List<Currency> GetAllCurrencies()
        {
            return _context.Currencies.ToList();
        }

        // Obtener monedas favoritas de un usuario
        public List<FavoriteCurrency> GetFavoriteCurrencies(int userId)
        {
            return _context.FavoriteCurrencies.Where(f => f.UserId == userId).ToList();
        }


        public float ConvertCurrency(User user, float amount, ConversionDto toConvert)
        {
            if (toConvert.ICfromConvert <= 0 || toConvert.ICtoConvert <= 0)
                throw new Exception("Invalid conversion rates.");

            return amount * toConvert.ICfromConvert / toConvert.ICtoConvert;
        }

        public void CreateCurrency(int loggedUserId, CreateAndUpdateCurrencyDto dto)
        {
            if (_context.Currencies.Any(c => c.Legend == dto.Legend))
                throw new Exception("Currency already exists.");

            var newCurrency = new Currency
            {
                Legend = dto.Legend,
                Symbol = dto.Symbol,
                IC = dto.IC,
                UserId = loggedUserId
            };

            _context.Currencies.Add(newCurrency);
            _context.SaveChanges();
        }

        public void UpdateCurrency(int currencyId, CreateAndUpdateCurrencyDto dto)
        {
            var currencyToUpdate = _context.Currencies.SingleOrDefault(c => c.CurrencyId == currencyId);
            if (currencyToUpdate is null)
                throw new Exception("Currency not found.");

            // Solo se actualizan los campos necesarios con los datos del DTO
            currencyToUpdate.Legend = dto.Legend;
            currencyToUpdate.Symbol = dto.Symbol;
            currencyToUpdate.IC = dto.IC;

            _context.SaveChanges();  // Guardamos los cambios en la base de datos
        }

        public void DeleteCurrency(int currencyId)
        {
            var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyId == currencyId);
            if (currency == null)
                throw new Exception("Currency not found.");

            _context.Currencies.Remove(currency);
            _context.SaveChanges();  // Eliminamos la moneda de la base de datos
        }


        public void AddFavoriteCurrency(int loggedUserId, AddFavoriteDto dto)
        {
            if (_context.FavoriteCurrencies.Any(f => f.Legend == dto.Legend && f.UserId == loggedUserId))
                throw new Exception("Currency is already in favorites.");

            var newFavorite = new FavoriteCurrency
            {
                Legend = dto.Legend,
                Symbol = dto.Symbol,
                IC = dto.IC,
                UserId = loggedUserId
            };

            _context.FavoriteCurrencies.Add(newFavorite);
            _context.SaveChanges();
        }

        public void RemoveFavoriteCurrency(int favoriteCurrencyId)
        {
            var currencyToRemove = _context.FavoriteCurrencies.SingleOrDefault(c => c.FavoriteCurrencyId == favoriteCurrencyId);
            if (currencyToRemove is null)
                throw new Exception("Favorite currency not found.");

            _context.FavoriteCurrencies.Remove(currencyToRemove);
            _context.SaveChanges();
        }
    }
}
