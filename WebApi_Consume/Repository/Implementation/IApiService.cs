namespace WebApi_Consume.Repository.Implementation
{
    public interface IApiService
    {
        //Task<T> GetAsync<T>(string url, string token);
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object data, string token);
        Task<T> PutAsync<T>(string url, object data, string token);
        Task<T> PatchAsync<T>(string url, object data, string token);
        Task<T> DeleteAsync<T>(string url, string token); // Update here
    }
}
