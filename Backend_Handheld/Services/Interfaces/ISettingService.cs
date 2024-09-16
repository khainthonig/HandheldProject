using HandheldProject.Entities.DataTransferObjects.Setting;

namespace HandheldProject.Services.Interfaces
{
    public interface ISettingService
    {
        Task<bool> Create(SettingCreateDto create);
        Task<List<SettingDto>> GetAll();
        Task<SettingDto> GetById(int id);
        Task<bool> Update(SettingUpdateDto updateDto);
        Task<SettingDto> GetByClassificationAndUser(int classificationId, int userId);
        Task<List<SettingDto>> Search(SettingSearchDto search);
    }
}
