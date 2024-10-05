using CurrencyConvert.Entities;
using CurrencyConvert.Models.Dtos;
using CurrencyConvert.Data;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConvert.Services.Implementations
{
    public class UserService 
    {
        private readonly CurrencyContext _context; // Dependiendo de cómo tengas configurado tu contexto de base de datos

        public UserService(CurrencyContext context)
        {
            _context = context;
        }

        public void Create(CreateAndUpdateUserDto dto)
        {
            // Aquí podrías realizar validaciones de DTO si es necesario
            var newUser = new User()
            {
                Email = dto.Email,
                Password = dto.Password
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        // Validación del usuario para el login
        public User? ValidateUser(AuthenticationRequestDto authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.Email == authRequestBody.Email && p.Password == authRequestBody.Password);
        }

    }
}


