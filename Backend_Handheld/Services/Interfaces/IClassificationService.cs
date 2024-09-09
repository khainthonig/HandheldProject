using Backend_Handheld.Entities.DataTransferObjects.Classification;

namespace Backend_Handheld.Services.Interfaces
{
    public interface IClassificationService
    {
        Task<bool> Create(ClassificationCreateDto classification);
        Task<List<ClassificationDto>> GetAll();
        Task<ClassificationDto> GetById(int id);
        Task<List<ClassificationDto>> Search(ClassificationSearchDto search);
    }
}
