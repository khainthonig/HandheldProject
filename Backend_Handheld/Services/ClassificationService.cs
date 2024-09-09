using Backend_Handheld.Entities.DataTransferObjects.Classification;
using Backend_Handheld.Entities.DataTransferObjects.User;
using Backend_Handheld.Entities.Models;
using Backend_Handheld.Repositories.Interfaces;
using Backend_Handheld.Services.Interfaces;
using Mapster;

namespace Backend_Handheld.Services
{
    public class ClassificationService : IClassificationService
    {
        private readonly IRepositoryManager _repositoryManager;
        public ClassificationService(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

        public async Task<bool> Create(ClassificationCreateDto createClassification)
        {
            var classification = createClassification.Adapt<Classification>();
            classification.CreatedDate = DateTime.UtcNow;
            var result = await _repositoryManager.ClassificationRepository.Create(classification);
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ClassificationDto>> GetAll()
        {
            var result = await _repositoryManager.ClassificationRepository.GetAll();
            var resultDto = result.Adapt<List<ClassificationDto>>();
            return await FilterData(resultDto);
        }

        public async Task<ClassificationDto> GetById(int id)
        {
            var result = await _repositoryManager.ClassificationRepository.GetById(id);
            var resultDto = result.Adapt<ClassificationDto>();
            return resultDto;
        }

        public async Task<List<ClassificationDto>> Search(ClassificationSearchDto search)
        {
            var result = await _repositoryManager.ClassificationRepository.Search(search);
            var resultDto = result.Adapt<List<ClassificationDto>>();
            return await FilterData(resultDto);
        }
        public async Task<List<ClassificationDto>> FilterData(List<ClassificationDto> lst)
        {
            return lst;
        }
    }
}
