using System;
using System.Collections.Generic;
using MonedaConvert.Entities;
using MonedaConvert.Models.Dtos;
using MonedaConvert.Data;

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
            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Subscription = Subscription.Free,
                totalConvertions = 10
                UserCurrencies = new List<UserCurrency>(),
                ConversionHistories = new List<Conversion>()
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        //Ver si esta logueado
        public User? ValidateUser(AuthenticationRequestDto authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.Email == authRequestBody.Email && p.Password == authRequestBody.Password);
        }

        public void DeleteUser(int userId)
        {
            _context.Users.Delete(userId);
        }

        public void AddFavoriteCurrency(int userId, string currencyId)
        {
            var user = _context.GetById(userId);
            var currency = _context.GetById(currencyId);
            if (user != null && currency != null)
            {
                if (!user.UserCurrencies.Any(fc => fc.CurrencyId == currencyId))
                {
                    user.UserCurrencies.Add(new UserCurrency { UserId = userId, CurrencyId = currencyId });
                    _userRepository.Update(user);
                }
            }
            else
            {
                throw new Exception("User or Currency not found");
            }
        }
    }
}


