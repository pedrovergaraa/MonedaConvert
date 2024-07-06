using System;
using System.Collections.Generic;
using MonedaConvert.Entities;
using MonedaConvert.Models.Dtos;
using MonedaConvert.Data;
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
                Password = dto.Password
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        //Ver si esta logueado
        public User? ValidateUser(AuthenticationRequestDto request)
        {
            return _context.Users.FirstOrDefault(p => p.Email == request.Email && p.Password == request.Password);
        }

    }
}


