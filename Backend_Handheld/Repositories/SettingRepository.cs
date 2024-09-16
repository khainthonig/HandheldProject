using Backend_Handheld.Entities.Models;
using Backend_Handheld.PostgreSqlHelper;
using HandheldProject.Entities.DataTransferObjects.Setting;
using HandheldProject.Entities.Models;
using HandheldProject.Repositories.Interfaces;

namespace HandheldProject.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly IDBService _dbService;

        public SettingRepository(IConfiguration configuration)
        {
            _dbService = new DbService(configuration);
        }

        public async Task<bool> Create(Setting result)
        {
            var results = await _dbService.EditData(
                "INSERT INTO public.setting ( user_id, classification_id, focus_value, exposure_value) " +
                "VALUES (@UserId, @ClassificationId, @FocusValue, @ExposureValue)", result);
            if (results > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Setting>> GetAll()
        {
            var lst = await _dbService.GetAll<Setting>("SELECT * FROM public.setting", new { });
            return lst;
        }

        public async Task<Setting> GetByClassificationAndUser(int classificationId, int userId)
        {
            var result = await _dbService.GetAsync<Setting>("SELECT * FROM public.setting " +
                        "WHERE classification_id=@ClassificationId and user_id=@UserId "
                        , new { ClassificationId = classificationId, UserId = userId });
            return result;
        }

        public async Task<Setting> GetById(int id)
        {
            var result = await _dbService.GetAsync<Setting>("SELECT * FROM public.setting where id=@Id", new { Id = id });
            return result;
        }

        public async Task<List<Setting>> Search(SettingSearchDto search)
        {
            var selectSql = "SELECT * FROM public.setting";

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
            var lst = await _dbService.GetAll<Setting>(selectSql + whereSql, search);

            return lst;
        }

        public async Task<bool> Update(Setting update)
        {
            var updateSql = " UPDATE public.setting SET  ";
            if(update.UserId != null)
            {
                updateSql += " user_id=@UserId, ";
            }
            if(update.ClassificationId != null)
            {
                updateSql += " classification_id=@ClassificationId, ";
            }
            if(update.FocusValue != null)
            {
                updateSql += " focus_value=@FocusValue, ";
            }
            if(update.ExposureValue != null)
            {
                updateSql += " exposure_value=@ExposureValue ";
            }
            else
            {
                if(updateSql.EndsWith(", ")) updateSql = updateSql.Remove(updateSql.Length - 2);
            }
            var whereSql = " WHERE id=@Id";
            var updates = await _dbService.EditData(updateSql + whereSql, update);
            return true;
        }
    }
}
