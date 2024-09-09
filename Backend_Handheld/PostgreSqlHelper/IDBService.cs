namespace Backend_Handheld.PostgreSqlHelper
{
    public interface IDBService
    {
        Task<T> GetAsync<T>(string command, object parms);
        Task<List<T>> GetAll<T>(string command, object parms);
        Task<int> EditData(string command, object parms);
        Task<int> ExecuteScalar(string command, object parms);
    }
}
