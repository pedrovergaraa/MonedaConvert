using CurrencyConvert.Models;
using CurrencyConvert.Models.Dtos;
using CurrencyConvert.Entities;
using System.Collections.Generic;

namespace CurrencyConvert.Services.Interfaces
{
    public interface ICurrencyService
    {
        string ConvertCurrency(double amount, int fromCurrencyId, int toCurrencyId, User user);

        bool CreateCurrency(Currency currency);

        bool EditCurrency(int currencyId, Currency updatedCurrency);

        bool DeleteCurrency(int currencyId);

        bool AddFavorite(int currencyId, int userId);

        bool RemoveFavorite(int currencyId, int userId);

        List<Currency> GetFavoriteCurrencies(int userId);
    }
}
