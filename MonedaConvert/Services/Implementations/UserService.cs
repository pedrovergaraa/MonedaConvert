using CurrencyConvert.Data;
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
            return null; // Usuario no válido
        }

        return user;
    }

    public void Create(CreateAndUpdateUserDto dto)
    {
        if (_context.Users.Any(u => u.Email == dto.Email))
            throw new InvalidOperationException("Email already in use.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Email = dto.Email,
            Password = hashedPassword,
            SubscriptionId = 1 // Default to Free subscription if not specified
        };

        _context.Users.Add(user);
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

        // Update subscription if specified, otherwise keep current one.
        user.SubscriptionId = dto.SubscriptionId ?? user.SubscriptionId;

        _context.SaveChanges();
    }

        public List<User> GetAllUsers()
        {
            return _context.Users
                           .Include(u => u.Currencies) // Carga de las monedas relacionadas con el usuario
                           .Include(u => u.FavoriteCurrencies) // Carga de los favoritos
                               .ThenInclude(fc => fc.Currency) // Carga de la moneda relacionada en FavoriteCurrency
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
