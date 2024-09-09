using Backend_Handheld.Entities.DataTransferObjects.Classification;
using Backend_Handheld.Entities.Models;
using Backend_Handheld.PostgreSqlHelper;
using Backend_Handheld.Repositories.Interfaces;

namespace Backend_Handheld.Repositories
{
    public class ClassificationRepository : IClassificationRepository
    {
        private readonly IDBService _dbService;

        public ClassificationRepository(IConfiguration configuration)
        {
            _dbService = new DbService(configuration);
        }
        public async Task<bool> Create(Classification classification)
        {
            var result = await _dbService.EditData(
                "INSERT INTO public.classification (name, description, image, created_date) " +
                "VALUES (@Name, @Description, @Image, @CreatedDate)", classification);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Classification>> GetAll()
        {
            var classifications = await _dbService.GetAll<Classification>("SELECT * FROM public.classification", new { });
            return classifications;
        }

        public async Task<Classification> GetById(int id)
        {
            var classification = await _dbService.GetAsync<Classification>("SELECT * FROM public.classification where id=@Id", new { Id = id });
            return classification;
        }

        public async Task<List<Classification>> Search(ClassificationSearchDto search)
        {
            var selectSql = "SELECT * FROM public.classification";

            var whereSql = " WHERE id is not null ";

            if (search.Id != null)
            {
                whereSql += " AND id = @Id";
            }
            if (search.Name != null)
            {
                whereSql += " AND name = @Name";
            }
            if (search.CreatedDate != null)
            {
                whereSql += " AND created_date > @CreatedDate";
            }
            if (search.IdLst != null && search.IdLst.Any())
            {
                whereSql += " and id=ANY(@IdLst)";
            }
            var classificationLst = await _dbService.GetAll<Classification>(selectSql + whereSql, search);

            return classificationLst;
        }

        public Task<bool> Update(Classification classification)
        {
            throw new NotImplementedException();
        }
    }
}
