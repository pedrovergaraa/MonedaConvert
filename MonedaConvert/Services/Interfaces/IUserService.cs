using MonedaConvert.Entities;
using MonedaConvert.Models.Dtos;

namespace MonedaConvert.Services.Interfaces
{
    public interface IUserService
    {
        void Create(CreateAndUpdateUserDto dto);
        User? ValidateUser(AuthenticationRequestDto authRequestBody);
    }
}
