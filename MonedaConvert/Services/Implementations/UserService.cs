using System;
using System.Collections.Generic;
using CurrencyConvert.Entities;
using CurrencyConvert.Models.Dtos;
using CurrencyConvert.Data;
using CurrencyConvert.Services.Interfaces;

namespace CurrencyConvert.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly CurrencyContext _context; // Dependiendo de cómo tengas configurado tu contexto de base de datos

        public UserService(CurrencyContext context)
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
                Password = dto.Password
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        // Validación del usuario para el login
        public User? ValidateUser(AuthenticationRequestDto authenticationRequest)
        {
            // Buscamos al usuario por su email
            var user = _context.Users
                .Include(u => u.Subscription) // Opcional: para incluir la información de suscripción
                .FirstOrDefault(u => u.Email == authenticationRequest.Email);

            // Si no encontramos al usuario o la contraseña no coincide
            if (user == null || user.Password != authenticationRequest.Password) // Aquí comparamos las contraseñas
            {
                return null; // Si no es válido, devolvemos null
            }

            return user; // Si es válido, devolvemos el usuario
        }

    }
}


