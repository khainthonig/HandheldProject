using Backend_Handheld.Entities.DataTransferObjects.Classification;
using Backend_Handheld.Entities.DataTransferObjects.Result;
using Backend_Handheld.Entities.DataTransferObjects.User;
using Backend_Handheld.Entities.Models;
using Backend_Handheld.Repositories.Interfaces;
using Backend_Handheld.Services.Interfaces;
using Mapster;
using System.Globalization;
using System.Net;

namespace Backend_Handheld.Services
{
    public class ResultService : IResultService
    {
        private readonly IRepositoryManager _repositoryManager;
        public ResultService(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

        public async Task<bool> Create(ResultCreateDto createDto)
        {
            var result = createDto.Adapt<Result>();
            result.CreatedDate = DateTime.UtcNow;
            var resultDto = await _repositoryManager.ResultRepository.Create(result);
            if (resultDto)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ResultDto>> GetAll()
        {
            var result = await _repositoryManager.ResultRepository.GetAll();
            var resultDto = result.Adapt<List<ResultDto>>();
            return await FilterData(resultDto);
        }

        public async Task<ResultDto> GetById(int id)
        {
            var result = await _repositoryManager.ResultRepository.GetById(id);
            var resultDto = result.Adapt<ResultDto>();
            return resultDto;
        }

        public async Task<List<ResultDto>> Search(ResultSearchDto search)
        {
            var result = await _repositoryManager.ResultRepository.Search(search);
            var resultDto = result.Adapt<List<ResultDto>>();
            return await FilterData(resultDto);
        }
        public async Task<List<ResultDto>> FilterData(List<ResultDto> lst)
        {
            if(lst?.Count() > 0)
            {
                var userIdLst = lst.Where(x => x.UserId.HasValue).Select(x => x.UserId.GetValueOrDefault()).ToList();
                if (userIdLst.Count > 0)
                {
                    var searchUser = new UserSearchDto()
                    {
                        IdLst = userIdLst
                    };
                    var users = (await _repositoryManager.UserRepository.Search(searchUser))?.ToDictionary(x => x.Id, x => x.Username);
                    if (users?.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            if (item.UserId.HasValue && users.ContainsKey(item.UserId.Value))
                            {
                                item.Username = users[item.UserId.Value];
                            }
                        }
                    }
                }
                var classificationIdLst = lst.Where(x => x.ClassificationId.HasValue).Select(x => x.ClassificationId.GetValueOrDefault()).ToList();
                if (classificationIdLst.Count > 0)
                {
                    var searchClassification = new ClassificationSearchDto()
                    {
                        IdLst = classificationIdLst
                    };
                    var classifications = (await _repositoryManager.ClassificationRepository.Search(searchClassification))?.ToDictionary(x => x.Id, x => x.Name);
                    if (classifications?.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            if (item.ClassificationId.HasValue && classifications.ContainsKey(item.ClassificationId.Value))
                            {
                                item.ClassificationName = classifications[item.ClassificationId.Value];
                            }
                        }
                    }
                }
            }
            return lst;
        }

        public async Task<bool> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Images", timestamp);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var filePath = Path.Combine(uploadPath, file.FileName);
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while uploading file: {ex.Message}");
                return false;
            }
        }
    }
}
