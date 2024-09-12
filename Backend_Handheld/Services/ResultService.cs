using Backend_Handheld.Entities.DataTransferObjects.Classification;
using Backend_Handheld.Entities.DataTransferObjects.Result;
using Backend_Handheld.Entities.DataTransferObjects.User;
using Backend_Handheld.Entities.Models;
using Backend_Handheld.Repositories.Interfaces;
using Backend_Handheld.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HandheldProject.Entities.Const;
using HandheldProject.Entities.DataTransferObjects.Results;
using Mapster;
using System.Globalization;
using System.Net;

namespace Backend_Handheld.Services
{
    public class ResultService : IResultService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly Cloudinary _cloudinary;
        public ResultService(IRepositoryManager repositoryManager)
        {
            this._repositoryManager = repositoryManager;
            var account = new Account(
                CloudinaryKey.CLOUD_NAME,
                CloudinaryKey.API_KEY,
                CloudinaryKey.API_SECRET
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<bool> Create(ResultCreateDto createDto)
        {
            var result = createDto.Adapt<Result>();
            string currentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            result.CreatedDate = DateTime.SpecifyKind(DateTime.ParseExact(currentTime, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture), DateTimeKind.Local);
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
            if (lst?.Count() > 0)
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
            return lst.OrderByDescending(i => i.CreatedDate).ToList();
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

        public async Task<ResultConditionDto> GetListByCondition(ResultSearchDto search)
        {
            var lst = new ResultConditionDto();
            var results = await _repositoryManager.ResultRepository.Search(search);
            var result = await FilterData(results.Adapt<List<ResultDto>>());

            lst.Dates = result.Select(y => y.CreatedDate.ToString("yyyy-MM-dd")).Distinct().ToList();
            lst.Results = await FilterData(result.Adapt<List<ResultDto>>());

            if (search.UserId != null)
            {
                lst.Results = lst.Results.Where(r => r.UserId == search.UserId).ToList();
            }
            if (search.CreatedDate != null)
            {
                var classifications = await GetClassifications();
                lst.Results = lst.Results.Where(r =>
                    r.CreatedDate.Date.ToString("yyyy-MM-dd") == search.CreatedDate.Value.Date.ToString("yyyy-MM-dd") 
                ).ToList();
                lst.CreatedDate = (DateTime)search.CreatedDate;
                lst.Dates = lst.Results.Select(m => m.ClassificationId.ToString()).Distinct().ToList();
                lst.Dates = lst.Dates.Select(id =>
                    classifications.TryGetValue(int.Parse(id), out var name) ? name : "undefined")
                    .ToList();
            }
            //if (search.Year != null)
            //{
            //    lst.Results = result.Where(r => r.CreatedDate.Year == search.Year
            //                                ).Distinct().Adapt<List<ResultDto>>();
            //    lst.Year = (int)search.Year;
            //    lst.Dates = lst.Results.Select(y => y.CreatedDate.Month.ToString()).Distinct().ToList();
            //}

            //if (search.Month != null)
            //{
            //    lst.Results = result.Where(r => r.CreatedDate.Year == search.Year
            //                && r.CreatedDate.Month == search.Month
            //                ).Distinct().Adapt<List<ResultDto>>();
            //    lst.Month = (int)search.Month;
            //    lst.Dates = lst.Results.Select(m => m.CreatedDate.Day.ToString()).Distinct().ToList();
            //}

            //if (search.Day != null)
            //{
            //    var classifications = await GetClassifications();
            //    lst.Day = (int)search.Day;

            //    lst.Results = result.Where(r => r.CreatedDate.Year == search.Year
            //                && r.CreatedDate.Month == search.Month
            //                && r.CreatedDate.Day == search.Day
            //                ).Distinct().Adapt<List<ResultDto>>();
            //    lst.Dates = lst.Results.Select(m => m.ClassificationId.ToString()).Distinct().ToList();
            //    lst.Dates = lst.Dates.Select(id =>
            //        classifications.TryGetValue(int.Parse(id), out var name) ? name : "undefined")
            //        .ToList();
            //}

            if (search.ClassificationId != null)
            {
                lst.Results = lst.Results.Where(r => 
                                             r.ClassificationId == search.ClassificationId).Distinct().Adapt<List<ResultDto>>();
                lst.ClassificationId = (int)search.ClassificationId;
                lst.CountOK = lst.Results.Count(r => r.Status == true);
                lst.CountNG = lst.Results.Count(r => r.Status == false);
            }

            if (search.Status != null)
            {
                lst.Status = (bool)search.Status;
                lst.Results = lst.Results.Where(r => r.Status == search.Status).Adapt<List<ResultDto>>();
            }

            return lst;

        }
        private async Task<Dictionary<int, string>> GetClassifications()
        {
            var classifications = await _repositoryManager.ClassificationRepository.GetAll();
            return classifications.ToDictionary(c => c.Id, c => c.Name);
        }


        public async Task<string> UploadToCloudDinary(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Image is not format");
            }

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        PublicId = $"results/{DateTime.Now:yyyyMMdd_HHmmss}_{Path.GetFileNameWithoutExtension(file.FileName)}",
                        Transformation = new Transformation().Quality("auto").FetchFormat("auto")
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.StatusCode == HttpStatusCode.OK)
                    {
                        return uploadResult.SecureUrl.ToString();
                    }
                    else
                    {
                        throw new Exception($"Upload image failed: {uploadResult.Error.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading to Cloudinary: {ex.Message}");
                throw;
            }
        }
    }
}
