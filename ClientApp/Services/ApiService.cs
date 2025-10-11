using ClientApp.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ClientApp.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public void SetBearerToken(string token)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync($"{Constants.URL}{endpoint}");
            return response.IsSuccessStatusCode;
        }

        public async Task<T?> GetAsync<T>(string uri) // Updated to match the interface  
        {
            var response = await _httpClient.GetAsync($"{Constants.URL}{uri}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<T>();
            /*if (result is null)
            {
                throw new InvalidOperationException("The response content is null.");
            }*/
            return result;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var response = await _httpClient.PostAsJsonAsync($"{Constants.URL}{endpoint}", data);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TResponse>();
            if (result is null)
            {
                throw new InvalidOperationException("The response content is null.");
            }
            return result;
        }

        public Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            throw new NotImplementedException();
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var loginRequest = new { Email = email, Password = password };
            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{Constants.URL}/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var token = JsonNode.Parse(json)?["token"]?["result"]?.ToString();
            return token;
        }

        public async Task SaveTokenASync(string token)
        {
            await SecureStorage.SetAsync("auth_token", token);
        }

        public async Task<string?> GetTokenAsync()
        {
            return await SecureStorage.GetAsync("auth_token");
        }
        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                SetBearerToken(token);
                var response = await _httpClient.GetAsync("api/auth/validate");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

    }
}
