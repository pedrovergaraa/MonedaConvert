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

        public double Convert(User user, double amount, ToConvert toConvert)
        {
            double result = amount * toConvert.ICfromConvert / toConvert.ICtoConvert;
            return result;
        }

        public void CreateCurrency(int loggedUserId, CreateAndUpdateCurrencyDto dto)
        {
            UserCurrency newCurrency = new UserCurrency()
            {
                Legend = dto.Legend,
                Symbol = dto.Symbol,
                IC = dto.IC,
                UserId = loggedUserId
            };
            _context.UserCurrencies.Add(newCurrency);
            _context.SaveChanges();
        }

        public void UpdateCurrency(int currencyId, string legend, CreateAndUpdateCurrencyDto dto)
        {
            User? currencyToUpdate = _context.Currencies.SingleOrDefault(c => c.Id == currencyId);
            FavoriteCurrency? favoriteCurrencyToUpdate = _context.FavoriteCurrencies.SingleOrDefault(f => f.Legend == legend);

            if (currencyToUpdate is not null)
            {
                // Edit user currency
                currencyToUpdate.Legend = dto.Legend;
                currencyToUpdate.Symbol = dto.Symbol;
                currencyToUpdate.IC = dto.IC;

                if (favoriteCurrencyToUpdate is not null)
                {
                    // Edit favorite currency
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

        public void DeleteCurrency(int currencyId, string legend)
        {
            FavoriteCurrency? favoriteCurrencyToDelete = _context.FavoriteCurrencies.SingleOrDefault(f => f.Legend == legend);
            if (favoriteCurrencyToDelete is not null)
            {
                _context.FavoriteCurrencies.Remove(favoriteCurrencyToDelete);
            }
            _context.UserCurrencies.Remove(_context.UserCurrencies.Single(c => c.Id == currencyId));
            _context.SaveChanges();
        }

        public void AddFavoriteCurrency(int loggedUserId, AddFavoriteDto dto)
        {
            List<FavoriteCurrency> currencies = _context.FavoriteCurrencies.Where(u => u.UserId == loggedUserId).ToList();

            bool exists = false;

            foreach (FavoriteCurrency currency in currencies)
            {
                if (dto.Legend == currency.Legend)
                {
                    exists = true;
                    break;
                }
            }

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

        public void RemoveFavorite(int currencyId)
        {
            _context.FavoriteCurrencies.Remove(_context.FavoriteCurrencies.Single(c => c.Id == currencyId));
            _context.SaveChanges();
        }
    }
}
