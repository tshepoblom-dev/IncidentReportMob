using ClientApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Viewmodels
{
    public partial class DashboardViewmodel : ObservableObject
    {
        [RelayCommand]
        public async Task GoToHouseholdAsync()
        {
            await Shell.Current.GoToAsync(nameof(HouseholdList));
        }

        [RelayCommand]
        public async Task GoToIncidentAsync()
        {
            await Shell.Current.GoToAsync(nameof(IncidentList));
        }

        [RelayCommand]
        public async Task GoToProfileAsync()
        {
          //  await Shell.Current.GoToAsync(nameof(ProfilePage));
        }

        [RelayCommand]
        public async Task LogoutAsync()
        {
            SecureStorage.Remove("auth_token");
            await Shell.Current.GoToAsync("//MainPage");
        }

    }
}
