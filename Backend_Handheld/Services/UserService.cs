using Backend_Handheld.Entities.DataTransferObjects.User;
using Backend_Handheld.Entities.Models;
using Backend_Handheld.Repositories.Interfaces;
using Backend_Handheld.Services.Interfaces;
using Hangfire.PostgreSql.Utils;
using Mapster;

namespace Backend_Handheld.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        public UserService(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

        public async Task<bool> Create(UserCreateDto createUserDto)
        {
            var user = createUserDto.Adapt<User>();
            user.CreatedDate = DateTime.UtcNow;         
            var result = await _repositoryManager.UserRepository.Create(user);
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<List<UserDto>> GetAll()
        {
            var result = await _repositoryManager.UserRepository.GetAll();
            var resultDto = result.Adapt<List<UserDto>>();
            return await FilterData(resultDto);

        }

        public Task<UserDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> Login(string username, string password)
        {
            var userLogin = await _repositoryManager.UserRepository.Login(username, password);
            var userDto = userLogin.Adapt<UserDto>();
            return userDto;
        }

        public async Task<List<UserDto>> Search(UserSearchDto search)
        {
            var result = await _repositoryManager.UserRepository.Search(search);
            var resultDto = result.Adapt<List<UserDto>>();
            return await FilterData(resultDto);
        }

        public Task<bool> Update(UserUpdateDto user)
        {
            throw new NotImplementedException();
        }
        public async Task<List<UserDto>> FilterData(List<UserDto> lstUsers)
        {
            return lstUsers;
        }

        public async Task<UserDto> GetByUsername(string username)
        {
            var user = await _repositoryManager.UserRepository.GetByUsername(username);
            var userDto = user.Adapt<UserDto>();
            return userDto;
        }
    }
}
