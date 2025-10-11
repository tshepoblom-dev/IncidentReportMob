using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string uri);
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<bool> DeleteAsync(string endpoint);
        Task<string?> LoginAsync(string email, string password);
        Task SaveTokenASync(string token);
        Task<string?> GetTokenAsync();
        void SetBearerToken(string token);
        Task<bool> ValidateTokenAsync(string token);

    }
}
