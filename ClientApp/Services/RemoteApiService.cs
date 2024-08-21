using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
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
        public async Task<bool> CreateIncidentAsync(Incident incident)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                // Add incident details as JSON
                var incidentJson = JsonSerializer.Serialize(incident);
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

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error creating incident: {ex.Message}");
                
                
                Debug.WriteLine(ex.InnerException);
                return false;
            }
        }

        public Task<IEnumerable<Incident>> GetAllIncidentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IncidentCategory>> GetIncidentCategoriesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
