using Backend_Handheld.Repositories.Interfaces;
using Backend_Handheld.Services.Interfaces;

namespace Backend_Handheld.Services
{
    public class ServiceManager : IServiceManager
    {
        public readonly Lazy<IUserService> lazyUserService;
        public readonly Lazy<IClassificationService> lazyClassificationService;
        public readonly Lazy<IResultService> lazyResultService;
        public ServiceManager(IRepositoryManager repositoryManager, IConfiguration configuration,
                                IWebHostEnvironment webHostEnvironment)
        {
            lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager));
            lazyClassificationService = new Lazy<IClassificationService>(() => new ClassificationService(repositoryManager));
            lazyResultService = new Lazy<IResultService>(() => new ResultService(repositoryManager));
        }
        public IUserService UserService => lazyUserService.Value;

        public IClassificationService ClassificationService => lazyClassificationService.Value;

        public IResultService ResultService => lazyResultService.Value;
    }
}
