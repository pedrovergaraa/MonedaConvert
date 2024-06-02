//using System;
//using System.Collections.Generic;
//using MonedaConvert.Entities;
//using MonedaConvert.Dtos;

public class UserService : IUserService
{
    private readonly MonedaContext _context; // Dependiendo de cómo tengas configurado tu contexto de base de datos

    public UserService(MonedaContext context)
    {
        _context = context;
    }

    //public bool CheckIfUserExists(int userId)
    //{
    //    return _context.Users.Any(u => u.Id == userId);
    //}

    //public void Create(CreateAndUpdateUserDto dto)
    //{
    //    // Aquí podrías realizar validaciones de DTO si es necesario
    //    var newUser = new User
    //    {
    //        // Aquí asigna las propiedades del DTO a las propiedades del modelo de datos
    //        // Ejemplo: Name = dto.Name
    //    };
    //    _context.Users.Add(newUser);
    //    _context.SaveChanges();
    //}

    //public List<UserDto> GetAll()
    //{
    //    // Aquí podrías realizar cualquier filtrado o proyección que necesites
    //    // y luego mapear los resultados a UserDto
    //    var users = _context.Users.ToList();
    //    // Mapeo a UserDto
    //    var userDtos = new List<UserDto>();
    //    foreach (var user in users)
    //    {
    //        var userDto = new UserDto
    //        {
    //            // Mapeo de propiedades
    //        };
    //        userDtos.Add(userDto);
    //    }
    //    return userDtos;
    //}

    //public GetUserByIdDto? GetById(int userId)
    //{
    //    var user = _context.Users.Find(userId);
    //    if (user == null)
    //        return null;

    //    // Aquí podrías mapear el modelo de datos a GetUserByIdDto
    //    var userDto = new GetUserByIdDto
    //    {
    //        // Mapeo de propiedades
    //    };
    //    return userDto;
    //}

    //public void RemoveUser(int userId)
    //{
    //    var user = _context.Users.Find(userId);
    //    if (user != null)
    //    {
    //        _context.Users.Remove(user);
    //        _context.SaveChanges();
    //    }
    //}

    //public void Update(CreateAndUpdateUserDto dto, int userId)
    //{
    //    var user = _context.Users.Find(userId);
    //    if (user != null)
    //    {
    //        // Aquí podrías actualizar las propiedades del usuario con los valores del DTO
    //        // Ejemplo: user.Name = dto.Name;
    //        _context.SaveChanges();
    //    }
    //}

    //public User? ValidateUser(AuthenticationRequestDto authRequestBody)
    //{
    //    // Aquí podrías implementar la lógica de validación de usuario
    //    // Por ejemplo, buscar en la base de datos si las credenciales son válidas
    //    // y devolver el usuario si es válido, de lo contrario devolver null
    //    return _context.Users.FirstOrDefault(u => u.Username == authRequestBody.Username && u.Password == authRequestBody.Password);
    //}
}
