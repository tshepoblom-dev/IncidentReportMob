using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public interface IRemoteApiService
    {
        Task<IEnumerable<Incident>> GetAllIncidentsAsync();
        
        Task<bool> CreateIncidentAsync(Incident incident);

        Task<IEnumerable<IncidentCategory>> GetIncidentCategoriesAsync();
    }
}
