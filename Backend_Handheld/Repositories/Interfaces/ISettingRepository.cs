using HandheldProject.Entities.DataTransferObjects.Setting;
using HandheldProject.Entities.Models;

namespace HandheldProject.Repositories.Interfaces
{
    public interface ISettingRepository
    {
        Task<bool> Create(Setting result);
        Task<List<Setting>> GetAll();
        Task<Setting> GetById(int id);
        Task<Setting> GetByClassificationAndUser(int classificationId, int userId);
        Task<List<Setting>> Search(SettingSearchDto search);
        Task<bool> Update (Setting update);
    }
}
