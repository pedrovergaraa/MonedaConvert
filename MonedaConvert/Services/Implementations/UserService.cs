using MonedaConvert.Data;
using MonedaConvert.Entities;
using MonedaConvert.Models.Dtos;
using MonedaConvert.Models.Enum;
using MonedaConvert.Services.Interfaces;

namespace MonedaConvert.Services.Implementations
{
    public class UserService : IUserService
    {   
        private MonedaContext _context; 
        public UserService(MonedaContext context)
        {
            _context = context;
        }

        public GetUserByIdDto? GetById(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            if (user is not null) 
            {
                return new GetUserByIdDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Role = user.Role,
                };
            }
            return null;
        }

        public List<UserDto> GetAll() 
        { 
            return _context.Users.Select(u => new UserDto()
            {
                Id = u.Id,  
                FirstName= u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Role = u.Role,
                State = u.State
            }).ToList();    
        }

        public User? ValidateUser(AuthenticationRequestDto authenticationRequestBody)
        {
            return _context.Users.FirstOrDefault(p => p.UserName == authenticationRequestBody.UserName && p.Password == authenticationRequestBody.Password );
        }

        public void CreateUser(CreateAndUpdateUserDto dto)
        {
            User newUser = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                UserName = dto.UserName,
                State = State.Active,
                Role = Role.User,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void Update(CreateAndUpdateUserDto dto, int userId)
        {
            User userToUpdate = _context.Users.First(u => u.Id == userId);
            userToUpdate.FirstName = dto.FirstName;
            userToUpdate.Password = dto.Password;
            _context.SaveChanges();
        }

        public void RemoveUser(int userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new Exception("El cliente que intenta eliminar no existe");
            }

            if (user.FirstName != "Admin")
            {
                Delete(userId);
            }
            else
            {
                Archive(userId);
            }
        }
        public void Delete(int id) 
        {
            User? user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.State = State.Archived;
            }
            _context.SaveChanges();
        }

        public bool CheckIfUserExists(int userId)
        {
            User? user = _context.Users.FirstOrDefault(user => user.Id == userId);
            return user != null;
        }
    }
}
