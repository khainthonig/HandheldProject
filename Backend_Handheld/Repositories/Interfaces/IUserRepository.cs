using Backend_Handheld.Entities.DataTransferObjects.User;
using Backend_Handheld.Entities.Models;

namespace Backend_Handheld.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Create(User user);
        Task<List<User>> GetAll();
        Task<bool> Update(User user);
        Task<bool> Delete(int id);
        Task<User> GetById(int id);
        Task<User> GetByUsername(string username);
        Task<List<User>> Search(UserSearchDto search);
        Task<User> Login(string username, string password);
    }
}
