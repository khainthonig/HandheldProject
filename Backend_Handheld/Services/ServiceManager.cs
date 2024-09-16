using Backend_Handheld.Repositories.Interfaces;
using Backend_Handheld.Services.Interfaces;
using HandheldProject.Repositories.Interfaces;
using HandheldProject.Services;
using HandheldProject.Services.Interfaces;

namespace Backend_Handheld.Services
{
    public class ServiceManager : IServiceManager
    {
        public readonly Lazy<IUserService> lazyUserService;
        public readonly Lazy<IClassificationService> lazyClassificationService;
        public readonly Lazy<IResultService> lazyResultService;
        public readonly Lazy<ISettingService> lazySettingService;
        public ServiceManager(IRepositoryManager repositoryManager, IConfiguration configuration,
                                IWebHostEnvironment webHostEnvironment)
        {
            lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager));
            lazyClassificationService = new Lazy<IClassificationService>(() => new ClassificationService(repositoryManager));
            lazyResultService = new Lazy<IResultService>(() => new ResultService(repositoryManager));
            lazySettingService = new Lazy<ISettingService>(() => new SettingService(repositoryManager));
        }
        public IUserService UserService => lazyUserService.Value;

        public IClassificationService ClassificationService => lazyClassificationService.Value;

        public IResultService ResultService => lazyResultService.Value;

        public ISettingService SettingService => lazySettingService.Value;
    }
}
