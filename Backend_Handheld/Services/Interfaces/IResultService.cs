using Backend_Handheld.Entities.DataTransferObjects.Result;
using Microsoft.AspNetCore.Http;
namespace Backend_Handheld.Services.Interfaces
{
    public interface IResultService
    {
        Task<bool> Create(ResultCreateDto createDto);
        Task<bool> Upload(IFormFile file);
        Task<List<ResultDto>> GetAll();
        Task<ResultDto> GetById(int id);
        Task<List<ResultDto>> Search(ResultSearchDto search);
    }
}
