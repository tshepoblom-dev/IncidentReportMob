using ClientApp.Models;
using ClientApp.Services;
using ClientApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Viewmodels
{
    public partial class IncidentListViewmodel :  ObservableObject
    {
        
        public IncidentListViewmodel(IRemoteApiService remoteApiService) 
        {
            _remoteApiService = remoteApiService;
            Incidents = new ObservableCollection<Incident>();
            
            Init();
        }

        [ObservableProperty]
        ObservableCollection<Incident>? incidents;

        [ObservableProperty]
        Incident? selectedIncident;

        private IRemoteApiService _remoteApiService;

        async void Init()
        {
            try
            {
                var incidents = await _remoteApiService.GetAllIncidentsAsync();
                Incidents = new ObservableCollection<Incident>(incidents ?? new List<Incident>());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
            }
        } 

        [RelayCommand]
        async Task CreateIncidentAsync()
        {
            try
            {
                //ObservableCollection<IncidentDetails> incidentDetails = new ObservableCollection<IncidentDetails>((IEnumerable<IncidentDetails>)await _remoteApiService.GetAllIncidentsAsync());

                await Shell.Current.GoToAsync(nameof(IncidentDetails));

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);
            }
        }
    }
}
