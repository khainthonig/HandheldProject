using Backend_Handheld.Entities.DataTransferObjects.User;
using Backend_Handheld.Entities.Models;

namespace Backend_Handheld.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Create(UserCreateDto user);
        Task<List<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserDto> GetByUsername(string username);
        Task<bool> Update(UserUpdateDto user);
        Task<List<UserDto>> Search(UserSearchDto search);
        Task<UserDto> Login(string username, string password);
    }
}
