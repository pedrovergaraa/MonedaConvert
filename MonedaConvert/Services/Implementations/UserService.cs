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
<<<<<<< HEAD
                totalConvertions = 10,
                UserCurrencies = new List<UserCurrency>(),
=======
                Currencies = new List<Currency>(),
>>>>>>> e2bdb655fa3a6cb462f9a5ff3f22a671aaf579f8
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
        }
    }
}


