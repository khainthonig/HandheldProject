using HandheldProject.Repositories.Interfaces;

namespace Backend_Handheld.Repositories.Interfaces
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }
        IClassificationRepository ClassificationRepository { get; }
        IResultRepository ResultRepository { get; }
        ISettingRepository SettingRepository { get; }
    }
}
