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

        public double Convert(User user, double amount, Conversion toConvert)
        {
            double result = amount * toConvert.ICfromConvert / toConvert.ICtoConvert;
            return result;
        }

        public void CreateCurrency(CreateAndUpdateCurrencyDto dto, int loggedUserId)

        {
            // Implementación del método
            Currency newCurrency = new Currency()
            {
                Symbol = dto.Symbol,
                Legend = dto.Legend,
                IC = dto.IC,
                UserId = loggedUserId
            };
            _context.Currency.Add(newCurrency);
            _context.SaveChanges();
        }

        public List<Currency> GetAllByUser(int id) 
        {
            return _context.Currency.Include(c => c.User).Where(c => c.User.Id == id).Select(currency => new CurrencyDto()
            {
                Id = currency.Id,
                Legend = currency.Legend,
                Symbol = currency.Symbol,
                IC = currency.IC
            }).ToList();

        }

        //Boton de crear moneda

        
        //Lista de monedas para que el usuario pueda elegir
        //public List<UserCurrency> GetAllByUser(int userId)
        //{
        //    // Implementación del método
        //}


        public void RemoveCurrency(int currencyId)
        {
            _context.Currency.Remove(_context.Currencies.Single(c => c.Id == currencyId));
            _context.SaveChanges();
            // Implementación del método
        }
        //Editar moneda
        public void UpdateCurrency(string legend, CreateAndUpdateCurrencyDto dto, int currencyId)
        {
            // Implementación del método
            Currency? coinToUpdate = _context.Currency.SingleOrDefault(c => c.Id == currencyId);
            Currency? coinFavToUpdate = _context.Currency.SingleOrDefault(f => f.Legend == legend);

            if (coinToUpdate is not null)
            {
                //edit user coin 
                coinToUpdate.Legend = dto.Legend;
                coinToUpdate.Symbol = dto.Symbol;
                coinToUpdate.IC = dto.IC;

                if (coinFavToUpdate is not null)
                {
                    //edit fav coin
                    coinFavToUpdate.Legend = dto.Legend;
                    coinFavToUpdate.Symbol = dto.Symbol;
                    coinFavToUpdate.IC = dto.IC;
                }
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("El Id no coincide");
            }
        }

        //void RemoveFavoriteCurrency(int userId, string currencyCode);
        //decimal ConvertCurrency(int userId, string fromCurrencyCode, string toCurrencyCode, decimal amount);
        public void AddFavoriteCurrency(int loggedUserId, CurrencyDto dto)
        {
            List<Currency> coins = _context.Currency.Where(u => u.Id == loggedUserId).ToList();

            bool favCoin = false;

            foreach (Currency favCoin in coins)
            {
                if (dto.Leyenda == currency.Legend)
                {
                    favCoin = true;
                    break;
                }
            }

            if (favCoin == false)
            {
                Currency newFav = new Currency()
                {
                    Legend = dto.Legend,
                    Symbol = dto.Symbol,
                    IC = dto.IC,
                    userId = loggedUserId
                };
                _context.Currency.Add(newFav);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Esa moneda ya esta en favoritos");
            }
        }
}