using ClientApp.Models;
using ClientApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Microsoft.Maui.Networking;
using ClientApp.Converters;

namespace ClientApp.Viewmodels
{
    public partial class AddIncidentViewmodel : ObservableObject
    {
        //[ObservableProperty] int step = 0;

        [ObservableProperty] ObservableCollection<IncidentCategory> categories = new();
        [ObservableProperty] IncidentCategory? selectedCategory;
        [ObservableProperty] string? description;
        [ObservableProperty] string? address;
        [ObservableProperty] string? gpsLocation_Latitude;
        [ObservableProperty] string? gpsLocation_Longitude;
        [ObservableProperty] string? locationSummary;
        [ObservableProperty] ImageSource? capturedImage;
        [ObservableProperty] DateTime createdAt = DateTime.Now;

      //  private IApiService _apiService;
        private IRemoteApiService _remoteApiService;

        public bool HasImage => CapturedImage != null;
        public bool IsStep1 => Step == 0;
        public bool IsStep2 => Step == 1;
        public bool IsStep3 => Step == 2;
        public bool IsStep4 => Step == 3;
        public string NextButtonText => Step == 3 ? "Submit Now" : "Next";
        public bool CanGoBack => Step > 0;

        private int _step;
        public int Step
        {
            get => _step;
            set
            {
                if (SetProperty(ref _step, value))
                    OnStepChanged?.Invoke(_step - 1, value); // notify UI about the step change
            }
        }

        public event Func<int, int, Task>? OnStepChanged;

        public AddIncidentViewmodel(IRemoteApiService apiService)
        {
            _remoteApiService = apiService;
            LoadIncidentCategories();
        }
        private async void LoadIncidentCategories()
        {
            Categories = new ObservableCollection<IncidentCategory>(await _remoteApiService.GetIncidentCategoriesAsync() ?? new ObservableCollection<IncidentCategory>());
        }

        [RelayCommand]
        async Task CapturePhotoAsync()
        {
            var result = await MediaPicker.CapturePhotoAsync();
            if (result != null)
            {
                using var stream = await result.OpenReadAsync();
                CapturedImage = ImageSource.FromStream(() => stream);
                OnPropertyChanged(nameof(HasImage));
            }
        }

        [RelayCommand]
        async Task GetLocationAsync()
        {
            var location = await Geolocation.GetLastKnownLocationAsync()
                ?? await Geolocation.GetLocationAsync();
            if (location != null)
            {
                GpsLocation_Latitude = location.Latitude.ToString("F6");
                GpsLocation_Longitude = location.Longitude.ToString("F6");
            }
        }

        [RelayCommand]
        void Next()
        {
            if (Step < 3) Step++;
            else Submit();
            OnPropertyChanged(nameof(IsStep1));
            OnPropertyChanged(nameof(IsStep2));
            OnPropertyChanged(nameof(IsStep3));
            OnPropertyChanged(nameof(IsStep4));
            OnPropertyChanged(nameof(CanGoBack));
            OnPropertyChanged(nameof(NextButtonText));
        }

        [RelayCommand]
        void Previous()
        {
            if (Step > 0) Step--;
            OnPropertyChanged(nameof(IsStep1));
            OnPropertyChanged(nameof(IsStep2));
            OnPropertyChanged(nameof(IsStep3));
            OnPropertyChanged(nameof(IsStep4));
            OnPropertyChanged(nameof(CanGoBack));
            OnPropertyChanged(nameof(NextButtonText));
        }

        [RelayCommand]
        async void Submit()
        {

            var incidentFileBytes = await ImageHelper.ImageSourceToByteArrayAsync(CapturedImage);
            var incidentFilesBytes = new List<byte[]>();
            incidentFilesBytes.Add(incidentFileBytes);
            var incidentVm = new IncidentVm
            {
                //UserId = App.Current., // if you have a logged-in user
                IncidentCategoryId = SelectedCategory.Id,
                Description = Description?.Trim(),
                Address = Address?.Trim(),
                GPSLocation_Latitude = GpsLocation_Latitude,
                GPSLocation_Longitude = GpsLocation_Longitude,
                IncidentFilesBytes = incidentFilesBytes,
                CreatedAt = DateTime.UtcNow,
                Status = IncidentStatus.Pending
            };

            var current = Connectivity.Current.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // ✅ Device has internet access
                // TODO: Call your API service here
               
                await _remoteApiService.CreateIncidentAsync(incidentVm);
                App.Current.MainPage.DisplayAlert("Online", "Incident will be submitted to the server.", "OK");
            }
            else
            {
                // 🚫 No internet access
                // TODO: Save locally for later sync
                App.Current.MainPage.DisplayAlert("Offline", "No internet connection. Incident will be saved locally.", "OK");
            }
        }
    }
}
