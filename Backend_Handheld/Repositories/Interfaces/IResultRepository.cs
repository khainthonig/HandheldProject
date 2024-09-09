using Backend_Handheld.Entities.DataTransferObjects.Result;
using Backend_Handheld.Entities.Models;

namespace Backend_Handheld.Repositories.Interfaces
{
    public interface IResultRepository
    {
        Task<bool> Create(Result result);
        Task<List<Result>> GetAll();
        Task<Result> GetById(int id);
        Task<List<Result>> Search(ResultSearchDto search);
    }
}
