using Backend_Handheld.Entities.DataTransferObjects.Classification;
using Backend_Handheld.Entities.Models;

namespace Backend_Handheld.Repositories.Interfaces
{
    public interface IClassificationRepository
    {
        Task<bool> Create(Classification classification);
        Task<List<Classification>> GetAll();
        Task<bool> Update(Classification classification);
        Task<bool> Delete(int id);
        Task<Classification> GetById(int id);
        Task<List<Classification>> Search(ClassificationSearchDto search);
    }
}
