namespace Backend_Handheld.Services.Interfaces
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IClassificationService ClassificationService { get; }
        IResultService ResultService { get; }
    }
}
