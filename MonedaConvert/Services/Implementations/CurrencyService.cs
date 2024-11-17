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

        // Obtener todas las monedas disponibles
        public List<Currency> GetAllCurrencies()
        {
            return _context.Currencies.ToList();
        }

        public List<FavoriteCurrency> GetFavoriteCurrencies(int userId)
        {
           List<FavoriteCurrency> favoriteCurrencies = _context.FavoriteCurrencies.Where(u => u.UserId == userId).ToList();
            return favoriteCurrencies;
        }

        public List<Currency> GetDefaultCurrencies(int userId)
        {
            try
            {
                // Lista hardcodeada de monedas predeterminadas
                var defaultCurrencies = new List<Currency>
        {
            new Currency { CurrencyId = 1, Legend = "USD", Symbol = "$", IC = 1.0f, UserId = 0 },
            new Currency { CurrencyId = 2, Legend = "EUR", Symbol = "€", IC = 1.09f, UserId = 0 },
            new Currency { CurrencyId = 3, Legend = "GBP", Symbol = "£", IC = 0.8f, UserId = 0 },
            new Currency { CurrencyId = 4, Legend = "JPY", Symbol = "¥", IC = 110.0f, UserId = 0 },
            new Currency { CurrencyId = 5, Legend = "KC", Symbol = "kc", IC = 0.043f, UserId = 0 },
            new Currency { CurrencyId = 6, Legend = "ARS", Symbol = "$", IC = 0.002f, UserId = 0 }
        };

                // Devolver las monedas predeterminadas
                return defaultCurrencies;
            }
            catch (Exception ex)
            {
                // Capturar cualquier error
                throw new Exception($"Error al obtener las monedas predeterminadas: {ex.Message}");
            }
        }




        public Currency GetCurrencyById(int currencyId)
        {
            // Intenta obtener la moneda por ID desde la base de datos
            var currency = _context.Currencies.SingleOrDefault(c => c.CurrencyId == currencyId);
            return currency;  // Devuelve la moneda encontrada o null si no se encuentra
        }



        public float ConvertCurrency(User user, float amount, ConversionDto toConvert)
        {
            if (toConvert.ICfromConvert <= 0 || toConvert.ICtoConvert <= 0)
                throw new Exception("Invalid conversion rates.");

            return amount * toConvert.ICfromConvert / toConvert.ICtoConvert;
        }

       public void CreateCurrency(int userId, CreateAndUpdateCurrencyDto dto)
{
            // Validar que la moneda no exista para este usuario
            if (_context.Currencies.Any(c => c.Legend == dto.Legend && c.UserId == userId))
                throw new Exception("Currency already exists for this user.");

            var newCurrency = new Currency
            {
                Legend = dto.Legend,
                Symbol = dto.Symbol,
                IC = dto.IC,
                UserId = userId
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


        public void AddFavoriteCurrency(int LoggedUserId, AddFavoriteDto dto)
        {
            // Traemos las monedas favoritas del usuario logueado
            List<FavoriteCurrency> currencies = _context.FavoriteCurrencies.Where(u => u.UserId == LoggedUserId).ToList();

            bool isAlreadyFavorite = false;

            // Recorremos las monedas favoritas para ver si la que estamos agregando ya existe
            foreach (FavoriteCurrency currency in currencies)
            {
                if (dto.Legend == currency.Legend)
                {
                    isAlreadyFavorite = true;
                    break;
                }
            }

            // Si la moneda no está en la lista de favoritas, la agregamos
            if (!isAlreadyFavorite)
            {
                FavoriteCurrency newFavorite = new FavoriteCurrency()
                {
                    Legend = dto.Legend,
                    Symbol = dto.Symbol,
                    IC = dto.IC,
                    UserId = LoggedUserId   
                };
                _context.FavoriteCurrencies.Add(newFavorite);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Esa moneda ya está en tus favoritas.");
            }
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
