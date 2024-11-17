using CurrencyConvert.Entities;
using CurrencyConvert.Models.Dtos;
using CurrencyConvert.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Linq;

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
            // Buscar el usuario por email
            var user = _context.Users.Include(u => u.Currencies).FirstOrDefault(u => u.Email == dto.Email);

            // Verificar si el usuario existe y la contraseña coincide
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return null;

            return user;
        }


        public void Create(CreateAndUpdateUserDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                throw new Exception("El correo electrónico ya está en uso");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Email = dto.Email,
                Password = hashedPassword,
                SubscriptionId = dto.SubscriptionId ?? 1
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserById(int userId)
        {
            return _context.Users
                .Include(u => u.Currencies) // Asegurarse de cargar las monedas asociadas al usuario
                .FirstOrDefault(u => u.UserId == userId);
        }

        public void UpdateUser(int userId, CreateAndUpdateUserDto dto)
        {
            var user = _context.Users
                .Include(u => u.Currencies) // Asegurarse de cargar las monedas asociadas al usuario
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
                throw new Exception("Usuario no encontrado");

            user.Email = dto.Email ?? user.Email;
            if (!string.IsNullOrEmpty(dto.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            user.SubscriptionId = dto.SubscriptionId ?? user.SubscriptionId;

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Include(u => u.Currencies).ToList();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                throw new Exception("Usuario no encontrado");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
