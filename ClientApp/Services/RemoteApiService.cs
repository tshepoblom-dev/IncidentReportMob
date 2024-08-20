using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public class RemoteApiService : IRemoteApiService
    {
        public RemoteApiService() { }

        public Task<bool> CreateIncidentAsync(Incident incident)
        {
            throw new NotImplementedException();
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
