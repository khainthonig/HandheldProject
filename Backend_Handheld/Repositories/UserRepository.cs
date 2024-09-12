using Backend_Handheld.Entities.DataTransferObjects.User;
using Backend_Handheld.Entities.Models;
using Backend_Handheld.PostgreSqlHelper;
using Backend_Handheld.Repositories.Interfaces;

namespace Backend_Handheld.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDBService _dbService;

        public UserRepository(IConfiguration configuration)
        {
            _dbService = new DbService(configuration);
        }
        public async Task<bool> Create(User user)
        {
            var result = await _dbService.EditData(
                "INSERT INTO public.user (username, password, operator_id, full_name, created_date, role) " +
                "VALUES (@Username, @Password, @OperatorId, @FullName, @CreatedDate, @Role)",user );

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _dbService.EditData("DELETE FROM public.user WHERE id = @Id", new { Id = id });
            return true;
        }

        public async Task<List<User>> GetAll()
        {
            var userList = await _dbService.GetAll<User>("SELECT * FROM public.user", new { });
            return userList;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _dbService.GetAsync<User>("SELECT * FROM public.user where id=@Id", new { Id = id });
            return user;
        }

        public async Task<User> GetByUsername(string username)
        {
            var user = await _dbService.GetAsync<User>("SELECT * FROM public.user where username=@Username", new { Username = username });
            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = new();
            user.Username = username;
            user.Password = password;
            var selectSql = " SELECT * FROM public.user ";

            var whereSql = " where 0=0 ";
            if (!string.IsNullOrEmpty(username))
            {
                whereSql += " and username=@Username ";
            }
            if (!string.IsNullOrEmpty(password))
            {
                whereSql += " and password=@Password ";
            }
            var result = await _dbService.GetAsync<User>(selectSql + whereSql, user);
            return result;
        }

        public async Task<List<User>> Search(UserSearchDto search)
        {
            var selectSql = "SELECT * FROM public.user ";

            var whereSql = " WHERE id is not null ";

            if (search.Id != null)
            {
                whereSql += " AND id = @Id";
            }
            if (search.Username != null)
            {
                whereSql += " AND username=@Username";
            }   
            if (search.IdLst != null)
            {
                whereSql += " AND id=ANY(@IdLst)";
            }
            var userLst = await _dbService.GetAll<User>(selectSql + whereSql, search);

            return userLst;
        }

        public Task<bool> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
