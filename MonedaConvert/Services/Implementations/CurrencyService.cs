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

    public List<Currency> GetAllCurrencies()
    {
        return _context.Currencies.ToList();
    }

        public List<FavoriteCurrency> GetFavoriteCurrencies(int userId)
        {
            return _context.FavoriteCurrencies
                .Where(fc => fc.UserId == userId)
                .ToList();
        }

        public List<Currency> GetDefaultCurrencies()
    {
            return _context.Currencies
    .Where(c => c.UserId == 0 || c.UserId == null)
    .ToList();


        }

        public Currency GetCurrencyById(int currencyId)
    {
        return _context.Currencies.Find(currencyId);
    }

        public float ConvertCurrency(User user, float amount, int currencyFromId, int currencyToId)
        {
            // Obtener las tasas de cambio
            var currencyFrom = _context.Currencies.Find(currencyFromId);
            var currencyTo = _context.Currencies.Find(currencyToId);

            if (currencyFrom == null || currencyTo == null)
                throw new Exception("Moneda no encontrada.");

            // Verificar intentos disponibles
            if (user.Subscription.AllowedAttempts != int.MaxValue && user.Attempts >= user.Subscription.AllowedAttempts)
                throw new Exception("No tienes intentos restantes.");

            // Calcular conversión
            float conversionRate = currencyTo.IC / currencyFrom.IC;
            float convertedAmount = amount * conversionRate;

            // Registrar intento
            user.Attempts++;
            _context.SaveChanges();

            return convertedAmount;
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

    public void AddFavoriteCurrency(int userId, AddFavoriteDto dto)
    {
        // Verifica si la moneda ya es favorita para este usuario
        if (_context.FavoriteCurrencies.Any(c => c.UserId == userId && c.CurrencyId == dto.CurrencyId))
            throw new Exception("Currency is already a favorite.");

        var favoriteCurrency = new FavoriteCurrency
        {
            UserId = userId,
            CurrencyId = dto.CurrencyId 
        };

        _context.FavoriteCurrencies.Add(favoriteCurrency);
        _context.SaveChanges();
    }

    // Método para eliminar una moneda favorita
    public void RemoveFavoriteCurrency(int favoriteCurrencyId)
    {
        var favoriteCurrency = _context.FavoriteCurrencies.Find(favoriteCurrencyId);
        if (favoriteCurrency == null)
            throw new Exception("Favorite currency not found.");

        _context.FavoriteCurrencies.Remove(favoriteCurrency);
        _context.SaveChanges();
    }
}
}
