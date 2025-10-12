using ClientApp.Models;
using ClientApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Viewmodels
{
    public partial class HouseholdListViewmodel : ObservableObject
    {
        [ObservableProperty] ObservableCollection<Household> households;
        [ObservableProperty] Household selectedHousehold;
        private readonly IApiService _apiService;
        public HouseholdListViewmodel(IApiService apiService)
        {
            _apiService = apiService;
            LoadHouseholdsAsync();
        }
        public async void LoadHouseholdsAsync()
        {
            var households = await _apiService.GetAsync<List<Household>>("/api/HouseholdsApi");
            if (households != null)
            {
                Households = new ObservableCollection<Household>(households);
            }
        }
        [RelayCommand]
        public async void AddHousehold()
        {
            await Shell.Current.GoToAsync("//AddHouseholdPage");
        }

        [RelayCommand]
        public async void ViewHousehold(Household household)
        {
            if (household == null) return;
            var navigationParameter = new Dictionary<string, object>
            {
                { "Household", household }
            };
            //await Shell.Current.GoToAsync(nameof(Views.HouseholdDetailsPage), navigationParameter);

        }
    }
}
