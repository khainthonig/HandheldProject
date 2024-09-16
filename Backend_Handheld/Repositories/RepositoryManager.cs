using Backend_Handheld.Repositories.Interfaces;
using HandheldProject.Repositories;
using HandheldProject.Repositories.Interfaces;

namespace Backend_Handheld.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly  Lazy<IUserRepository> lazyUserRepository;
        private readonly  Lazy<IClassificationRepository> lazyClassificationRepository;
        private readonly Lazy<IResultRepository> lazyResultRepository;
        private readonly Lazy<ISettingRepository> lazySettingRepository;

        public RepositoryManager(IConfiguration configuration)
        {
            lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(configuration));
            lazyClassificationRepository = new Lazy<IClassificationRepository>(() => new ClassificationRepository(configuration));
            lazyResultRepository = new Lazy<IResultRepository>(() => new ResultRepository(configuration));
            lazySettingRepository = new Lazy<ISettingRepository>(() => new SettingRepository(configuration));
        }

        public IUserRepository UserRepository => lazyUserRepository.Value;

        public IClassificationRepository ClassificationRepository => lazyClassificationRepository.Value;

        public IResultRepository ResultRepository => lazyResultRepository.Value;

        public ISettingRepository SettingRepository => lazySettingRepository.Value;
    }
}
