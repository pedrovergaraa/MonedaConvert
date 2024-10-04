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

        // Función para convertir moneda
        public decimal ConvertCurrency(User user, decimal amount, ConversionDto toConvert)
        {
            decimal result = amount * toConvert.ICfromConvert / toConvert.ICtoConvert;
            return result;
        }

        // Función para crear una moneda
        public void CreateCurrency(int loggedUserId, CreateAndUpdateCurrencyDto dto)
        {
            Currency newCurrency = new Currency()
            {
                Legend = dto.Legend,
                Symbol = dto.Symbol,
                IC = (decimal)dto.IC,
                UserId = loggedUserId
            };
            _context.Currencies.Add(newCurrency);
            _context.SaveChanges();
        }

        // Función para actualizar moneda
        public void UpdateCurrency(int currencyId, string legend, CreateAndUpdateCurrencyDto dto)
        {
            Currency? currencyToUpdate = _context.Currencies.SingleOrDefault(c => c.CurrencyId == currencyId);
            FavoriteCurrency? favoriteCurrencyToUpdate = _context.FavoriteCurrencies.SingleOrDefault(f => f.Legend == legend);

            if (currencyToUpdate is not null)
            {
                // Editar moneda del usuario
                currencyToUpdate.Legend = dto.Legend;
                currencyToUpdate.Symbol = dto.Symbol;
                currencyToUpdate.IC = (decimal)dto.IC;

                if (favoriteCurrencyToUpdate is not null)
                {
                    // Editar moneda favorita
                    favoriteCurrencyToUpdate.Legend = dto.Legend;
                    favoriteCurrencyToUpdate.Symbol = dto.Symbol;
                    favoriteCurrencyToUpdate.IC = dto.IC;
                }
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("The ID does not match");
            }
        }

        // Función para eliminar moneda
        public void DeleteCurrency(int currencyId, string legend)
        {
            FavoriteCurrency? favoriteCurrencyToDelete = _context.FavoriteCurrencies.SingleOrDefault(f => f.Legend == legend);
            if (favoriteCurrencyToDelete is not null)
            {
                _context.FavoriteCurrencies.Remove(favoriteCurrencyToDelete);
            }

            Currency? currencyToDelete = _context.Currencies.SingleOrDefault(c => c.CurrencyId == currencyId);
            if (currencyToDelete != null)
            {
                _context.Currencies.Remove(currencyToDelete);
            }
            _context.SaveChanges();
        }

        // Función para agregar una moneda favorita
        public void AddFavoriteCurrency(int loggedUserId, AddFavoriteDto dto)
        {
            List<FavoriteCurrency> currencies = _context.FavoriteCurrencies.Where(u => u.UserId == loggedUserId).ToList();

            bool exists = currencies.Any(currency => dto.Legend == currency.Legend);

            if (!exists)
            {
                FavoriteCurrency newFav = new FavoriteCurrency()
                {
                    Legend = dto.Legend,
                    Symbol = dto.Symbol,
                    IC = dto.IC,
                    UserId = loggedUserId
                };
                _context.FavoriteCurrencies.Add(newFav);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("This currency is already in favorites");
            }
        }

        // Función para eliminar una moneda favorita
        public void RemoveFavoriteCurrency(int currencyId)
        {
            FavoriteCurrency? currencyToRemove = _context.FavoriteCurrencies.SingleOrDefault(c => c.FavoriteCurrencyId == currencyId);
            if (currencyToRemove != null)
            {
                _context.FavoriteCurrencies.Remove(currencyToRemove);
                _context.SaveChanges();
            }
        }
    }
}
