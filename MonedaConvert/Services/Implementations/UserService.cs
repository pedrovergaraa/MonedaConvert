﻿using CurrencyConvert.Data;
using CurrencyConvert.Entities;
using CurrencyConvert.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace CurrencyConvert.Services.Implementations
{

public class UserService
{
    private readonly CurrencyContext _context;

    public UserService(CurrencyContext context)
    {
        _context = context;
    }

    public User ValidateUser(AuthenticationRequestDto dto)
    {
        var user = _context.Users.Include(u => u.Currencies)
                                 .FirstOrDefault(u => u.Email == dto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            return null; 
        }

        return user;
    }

        public void Create(CreateAndUpdateUserDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                throw new InvalidOperationException("El email ya está en uso.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var subscription = _context.Subscriptions.Find(dto.SubscriptionId)
                               ?? throw new InvalidOperationException("ID de suscripción inválido.");

            var user = new User
            {
                Email = dto.Email,
                Password = hashedPassword,
                SubscriptionId = subscription.SubId,
                Attempts = subscription.Conversions,
                Currencies = new List<Currency>(),
                FavoriteCurrencies = new List<FavoriteCurrency>()
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Asignar monedas por defecto al usuario recién creado, solo si no están ya asignadas
            AssignDefaultCurrenciesToUser(user.UserId);
        }


        private void AssignDefaultCurrenciesToUser(int userId)
        {
            // Obtener las monedas por defecto
            var defaultCurrencies = _context.Currencies.Where(c => c.IsDefault).ToList();

            foreach (var currency in defaultCurrencies)
            {
                // Verificar si el usuario ya tiene esta moneda asignada
                bool userHasCurrency = _context.Currencies.Any(c => c.UserId == userId && c.Symbol == currency.Symbol);

                if (!userHasCurrency)
                {
                    // Si el usuario no tiene esta moneda, agregarla
                    var userCurrency = new Currency
                    {
                        Legend = currency.Legend,
                        Symbol = currency.Symbol,
                        IC = currency.IC,
                        IsDefault = true,
                        UserId = userId
                    };
                    _context.Currencies.Add(userCurrency);
                }
            }

            // Guardar los cambios
            _context.SaveChanges();
        }

        public User GetUserById(int userId)
    {
        return _context.Users
            .Include(u => u.Currencies)
            .Include(u => u.Subscription)
            .FirstOrDefault(u => u.UserId == userId);
    }

    public void UpdateUser(int userId, CreateAndUpdateUserDto dto)
    {
        var user = _context.Users.Find(userId);

        if (user == null)
            throw new KeyNotFoundException("User not found.");

        user.Email = dto.Email ?? user.Email;
        
        if (!string.IsNullOrEmpty(dto.Password))
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        user.SubscriptionId = dto.SubscriptionId ?? user.SubscriptionId;

        _context.SaveChanges();
    }

        public List<User> GetAllUsers()
        {
            return _context.Users
                           .Include(u => u.Currencies) 
                           .Include(u => u.FavoriteCurrencies) 
                               .ThenInclude(fc => fc.Currency) 
                           .ToList();
        }


        public void DeleteUser(int userId)
    {
        var user = _context.Users.Find(userId);

        if (user == null)
            throw new KeyNotFoundException("User not found.");

        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}
}
