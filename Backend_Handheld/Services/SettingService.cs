using Backend_Handheld.Entities.DataTransferObjects.Classification;
using Backend_Handheld.Entities.DataTransferObjects.Result;
using Backend_Handheld.Entities.DataTransferObjects.User;
using Backend_Handheld.Entities.Models;
using Backend_Handheld.Repositories.Interfaces;
using HandheldProject.Entities.DataTransferObjects.Setting;
using HandheldProject.Entities.Models;
using HandheldProject.Services.Interfaces;
using Mapster;
using System.Globalization;

namespace HandheldProject.Services
{
    public class SettingService : ISettingService
    {
        private readonly IRepositoryManager _repositoryManager;
        public SettingService(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
        }

        public async Task<bool> Create(SettingCreateDto create)
        {
            var setting = create.Adapt<Setting>();
            var result = await _repositoryManager.SettingRepository.Create(setting);
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<List<SettingDto>> GetAll()
        {
            var result = await _repositoryManager.SettingRepository.GetAll();
            var resultDto = result.Adapt<List<SettingDto>>();
            return await FilterData(resultDto);
        }

        public async Task<SettingDto> GetById(int id)
        {
            var result = await _repositoryManager.SettingRepository.GetById(id);
            var resultDto = result.Adapt<SettingDto>();
            return (await FilterData(new(){ resultDto})).FirstOrDefault();
        }

        public async Task<List<SettingDto>> Search(SettingSearchDto search)
        {
            var result = await _repositoryManager.SettingRepository.Search(search);
            var resultDto = result.Adapt<List<SettingDto>>();
            return await FilterData(resultDto);
        }
        public async Task<List<SettingDto>> FilterData(List<SettingDto> lst)
        {
            if (lst.Count() > 0)
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

        public async Task<bool> Update(SettingUpdateDto updateDto)
        {
            var update = updateDto.Adapt<Setting>();
            var result = await _repositoryManager.SettingRepository.Update(update);
            return result;
        }

        public async Task<SettingDto> GetByClassificationAndUser(int classificationId, int userId)
        {
            var result = await _repositoryManager.SettingRepository.GetByClassificationAndUser(classificationId, userId);
            if(result == null) return null;
            var resultDto = result.Adapt<SettingDto>();
            return (await FilterData(new() { resultDto })).FirstOrDefault();
        }
    }
}
