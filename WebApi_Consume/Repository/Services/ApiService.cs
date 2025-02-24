using WebApi_Consume.Repository.Implementation;

namespace WebApi_Consume.Repository.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient )
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(300); // Increase to 200 seconds
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string url, string token, HttpContent? content = null)
        {
            var request = new HttpRequestMessage(method, url)
            {
                Content = content
            };

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return request;
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string url, HttpContent? content = null)
        {
            var request = new HttpRequestMessage(method, url)
            {
                Content = content
            };

            //request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return request;
        }

        //public async Task<T> GetAsync<T>(string url, string token)
        //{
        //    try
        //    {
        //        var request = CreateRequest(HttpMethod.Get, url, token);
        //        var response = await _httpClient.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        return await response.Content.ReadFromJsonAsync<T>();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                var request = CreateRequest(HttpMethod.Get, url);
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<T> PostAsync<T>(string url, object data, string token)
        {
            try
            {
                var request = CreateRequest(HttpMethod.Post, url, token, JsonContent.Create(data));
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<T> PutAsync<T>(string url, object data, string token)
        {
            try
            {
                var request = CreateRequest(HttpMethod.Put, url, token, JsonContent.Create(data));
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<T> PatchAsync<T>(string url, object data, string token)
        {
            try
            {
                var request = CreateRequest(HttpMethod.Patch, url, token, JsonContent.Create(data));
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<T> DeleteAsync<T>(string url, string token)
        {

            try
            {
                var request = CreateRequest(HttpMethod.Delete, url, token);
                var response = await _httpClient.SendAsync(request);

                // Ensure the response is successful before trying to deserialize
                response.EnsureSuccessStatusCode();

                // Deserialize and return the response content as type T
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
