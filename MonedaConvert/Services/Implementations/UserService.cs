using System;
using System.Collections.Generic;
using MonedaConvert.Entities;
using MonedaConvert.Models.Dtos;
using MonedaConvert.Data;
using MonedaConvert.Models.Enum;
using MonedaConvert.Services.Interfaces;

namespace MonedaConvert.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly MonedaContext _context; // Dependiendo de cómo tengas configurado tu contexto de base de datos

        public UserService(MonedaContext context)
        {
            _context = context;
        }

        //Crear usuario
        //Chequear si el usuario esta logueado

        public void Create(CreateAndUpdateUserDto dto)
        {
            // Aquí podrías realizar validaciones de DTO si es necesario
            var newUser = new User()
            {
                Email = dto.Email,
                Password = dto.Password,
                Subscription = Subscription.Free,
                totalConvertions = 10,
                Currencies = new List<Currency>(),
                ConversionHistories = new List<Conversion>()
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        //Ver si esta logueado
        public User? ValidateUser(AuthenticationRequestDto request)
        {
            return _context.Users.FirstOrDefault(p => p.Email == request.Email && p.Password == request.Password);
        }


        public void AddFavoriteCurrency(int userId, string currencyId)
        {
            //Implementacion del codigo
            // Buscar el usuario por su ID
            var user = _context.Users.Include(u => u.Currencies).FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            // Buscar la moneda por su ID
            var currency = _context.Currencies.FirstOrDefault(c => c.Id == currencyId);
            if (currency == null)
            {
                throw new Exception("Moneda no encontrada");
            }

            // Verificar si la moneda ya está en la lista de monedas favoritas del usuario
            if (user.Currencies.Any(c => c.Id == currencyId))
            {
                throw new Exception("La moneda ya está en la lista de monedas favoritas del usuario");
            }

            // Agregar la moneda a la lista de monedas favoritas del usuario
            user.Currencies.Add(currency);
            _context.SaveChanges();
        }
    }
}


