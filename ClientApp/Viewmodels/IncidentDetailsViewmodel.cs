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
            UploadedMedia = new ObservableCollection<IncidentMedia>();

            LoadIncidentCategories();
        }

        [ObservableProperty]
        private ObservableCollection<IncidentCategory> incidentCategories;

        [ObservableProperty]
        private IncidentCategory selectedIncidentCategory;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string address;

        [ObservableProperty]
        private ObservableCollection<IncidentMedia> uploadedMedia;

        [RelayCommand]
        private async Task LoadIncidentCategories()
        {
            var categories = await _remoteApiService.GetIncidentCategoriesAsync();
            foreach (var category in categories)
            {
                IncidentCategories.Add(category);
            }
        }

        [RelayCommand]
        private async Task UploadFiles()
        {
            // Code to open file picker and add selected files to UploadedMedia collection
            var filePickerResult = await FilePicker.PickMultipleAsync();
           
            // not sure of this
            foreach (var file in filePickerResult)
            {
                var incidentMedia = new IncidentMedia
                {
                    Content = File.ReadAllBytes(file.FullPath),
                    ImageSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(file.FullPath)))
                };
                UploadedMedia.Add(incidentMedia);
            } 
        }

        [RelayCommand]
        private async Task SubmitIncident()
        {
            var incident = new Incident
            {
                IncidentCategoryId = SelectedIncidentCategory.Id,
                Description = Description,
                Address = Address,

            };

            var isSuccess = await _remoteApiService.CreateIncidentAsync(incident);

            if (isSuccess) {
                try
                {
                    // Navigate back to the IncidentList view 
                    //await Shell.Current.GoToAsync(nameof(IncidentList));
                    await Shell.Current.GoToAsync("..");
                }
            catch (Exception e)
                {
                    Debug.WriteLine(e.InnerException);
                   
                }
            }
        }
    }
}
