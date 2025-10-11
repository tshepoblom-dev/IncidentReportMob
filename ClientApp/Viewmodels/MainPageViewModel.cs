using ClientApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientApp.Viewmodels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IApiService _apiService;
        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private string loginError = string.Empty;

        public RelayCommand LoginCommand { get; }
        public MainPageViewModel(IApiService apiService)
        {
            LoginCommand = new RelayCommand(async () => await OnLoginClicked());
            _apiService = apiService;
        }

        private bool CanLogin()
        {
            return !IsBusy && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        }

        private async Task OnLoginClicked()
        {
            try
            {
               // IsBusy = true;
                LoginError = string.Empty;
                var token = await _apiService.LoginAsync(Email, Password);
                if (string.IsNullOrEmpty(token))
                {
                    LoginError = "Invalid email or password. Please try again";
                    return;
                }
                await _apiService.SaveTokenASync(token);
                _apiService.SetBearerToken(token);
                // Navigate to the IncidentList page
                await Shell.Current.GoToAsync("//Dashboard");
            }
            catch (Exception ex)
            {
                LoginError = $"Login failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

    }
}
