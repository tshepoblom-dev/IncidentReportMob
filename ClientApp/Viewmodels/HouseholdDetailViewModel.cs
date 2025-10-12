using ClientApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Viewmodels
{
    public partial class HouseholdDetailViewModel : ObservableObject
    {
        [ObservableProperty] Household house_hold;
        public HouseholdDetailViewModel(Household household)
        {
            House_hold = household;            
        }

        [RelayCommand]
        public async Task Edit(Household household)
        {
            if (household == null)
            {

            }
        }
    }
}
