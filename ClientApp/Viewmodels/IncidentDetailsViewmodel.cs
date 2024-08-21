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
    public partial class IncidentDetailsViewmodel : ObservableObject
    {

        private readonly IRemoteApiService _remoteApiService;

        public IncidentDetailsViewmodel(IRemoteApiService remoteApiService)
        {
            _remoteApiService = remoteApiService;
            IncidentCategories = new ObservableCollection<IncidentCategory>();
            UploadedMedia = new ObservableCollection<byte[]>();

            LoadIncidentCategories();
        }

        [ObservableProperty]
        private ObservableCollection<IncidentCategory>? incidentCategories;

        [ObservableProperty]
        private IncidentCategory? selectedIncidentCategory;

        [ObservableProperty]
        private string? description;

        [ObservableProperty]
        private string? address;

        [ObservableProperty]
        private string? gps_latitude;

        [ObservableProperty]
        private string? gps_longitude;

        [ObservableProperty]
        private ObservableCollection<byte[]> uploadedMedia = new ObservableCollection<byte[]>();

        private async void LoadIncidentCategories()
        {

            IncidentCategories = new ObservableCollection<IncidentCategory>(await _remoteApiService.GetIncidentCategoriesAsync() ?? new ObservableCollection<IncidentCategory>());
        }

        [RelayCommand]
        private async Task UploadFiles()
        {
            // Code to open file picker and add selected files to UploadedMedia collection
            var filePickerResult = await FilePicker.PickMultipleAsync();
           
            // not sure of this
            foreach (var file in filePickerResult)
            {
                UploadedMedia.Add(File.ReadAllBytes(file.FullPath));
            } 
        }

        [RelayCommand]
        private async Task SubmitIncident()
        {
            try
            {
                var incident = new IncidentVm
                {
                    IncidentCategoryId = SelectedIncidentCategory?.Id,
                    Description = Description,
                    Address = Address,
                    CreatedAt = DateTime.UtcNow,
                    GPSLocation_Latitude = Gps_latitude,
                    GPSLocation_Longitude = Gps_longitude,
                    IncidentFilesBytes = new List<byte[]>(UploadedMedia ?? new ObservableCollection<byte[]>())
                };

                var isSuccess = await _remoteApiService.CreateIncidentAsync(incident);

                if (isSuccess) {
            
                        // Navigate back to the IncidentList view 
                        //await Shell.Current.GoToAsync(nameof(IncidentList));
                        await Shell.Current.GoToAsync("..");
              
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);

            }
        }
    }
}
