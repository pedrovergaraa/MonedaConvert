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
                Suscription = Suscription.Free,
                totalConvertions = 10
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        //Ver si esta logueado
        public User? ValidateUser(AuthenticationRequestDto authRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.Email == authRequestBody.Email && p.Password == authRequestBody.Password);
        }

    }
}


