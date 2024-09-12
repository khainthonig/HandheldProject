using Backend_Handheld.Entities.DataTransferObjects.Result;
using Backend_Handheld.Entities.Models;
using Backend_Handheld.PostgreSqlHelper;
using Backend_Handheld.Repositories.Interfaces;

namespace Backend_Handheld.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly IDBService _dbService;

        public ResultRepository(IConfiguration configuration)
        {
            _dbService = new DbService(configuration);
        }

        public async Task<bool> Create(Result result)
        {
            var results = await _dbService.EditData(
                "INSERT INTO public.result (classification_id, user_id, image, status, created_date) " +
                "VALUES (@ClassificationId, @UserId, @Image, @Status, @CreatedDate)", result);

            if (results > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Result>> GetAll()
        {
            var lst = await _dbService.GetAll<Result>("SELECT * FROM public.result", new { });
            return lst;
        }

        public async Task<Result> GetById(int id)
        {
            var result = await _dbService.GetAsync<Result>("SELECT * FROM public.result where id=@Id", new { Id = id });
            return result;
        }

        public async Task<List<Result>> Search(ResultSearchDto search)
        {
            var selectSql = "SELECT * FROM public.result";

            var whereSql = " WHERE id is not null ";

            if (search.Id != null)
            {
                whereSql += " AND id = @Id";
            }
            if (search.ClassificationId != null)
            {
                whereSql += " AND classification_id = @ClassificationId";
            }
            if (search.UserId != null)
            {
                whereSql += " AND user_id = @UserId";
            }
            if (search.CreatedDate != null)
            {
                whereSql += " AND DATE(created_date) = @CreatedDate";
            }
            var lst = await _dbService.GetAll<Result>(selectSql + whereSql, search);

            return lst;
        }
    }
}
