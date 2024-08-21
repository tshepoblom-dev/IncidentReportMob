using ClientApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public class RemoteApiService : IRemoteApiService
    {
        private readonly HttpClient _httpClient;
        public RemoteApiService(HttpClient httpClient) 
        {
            _httpClient = httpClient;

        }
        private async Task<string?> SendGETRequest(string uri)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, uri);
                HttpResponseMessage responseMessage = await _httpClient.SendAsync(message);
                responseMessage.EnsureSuccessStatusCode();
                var responseBody = await responseMessage.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
            return null;
        }
        public async Task<bool> CreateIncidentAsync(IncidentVm incidentvm)
        {
            try
            {
                /*   using var content = new MultipartFormDataContent();

                   // Add incident details as JSON
                   var incidentJson = JsonConvert.SerializeObject(incident);
                   var incidentContent = new StringContent(incidentJson, Encoding.UTF8, "application/json");
                   content.Add(incidentContent, "incident");

                   // Add media files
                   if (incident.MediaFiles != null && incident.MediaFiles.Any())
                   {
                       foreach (var media in incident.MediaFiles)
                       {
                           if (media.Content != null)
                           {
                               var byteArrayContent = new ByteArrayContent(media.Content);
                               byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                               content.Add(byteArrayContent, "mediaFiles", $"media_{media.Id}.jpg");
                           }
                       }
                   }

                   // POST request to API
                   var response = await _httpClient.PostAsync("api/incidents", content);

                   return response.IsSuccessStatusCode;*/


                HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync($"/api/IncidentsApi", incidentvm);
                return responseMessage.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
                return false;
            }
        }

        public async Task<IEnumerable<Incident>?> GetAllIncidentsAsync()
        {
            try
            {
                var responseBody = await SendGETRequest($"/api/IncidentsApi");
                if (responseBody != null)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Incident>>(responseBody);//
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
            return null;
        }

        public async Task<IEnumerable<IncidentCategory>?> GetIncidentCategoriesAsync()
        {
            try
            {
                var responseBody = await SendGETRequest($"/api/IncidentCategoriesApi/");
                if (responseBody != null)
                { 
                    return JsonConvert.DeserializeObject<IEnumerable<IncidentCategory>>(responseBody);//
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
            return null;
        }
    }
}
