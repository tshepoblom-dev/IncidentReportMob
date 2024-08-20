using ClientApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientApp.Viewmodels
{
    public partial class MainPageVM : ObservableObject
    {
        public ICommand LoginCommand { get; }
        public MainPageVM() 
        {
            LoginCommand = new Command(OnLoginClicked);

        }
        private async void OnLoginClicked()
        {
            // Navigate to the IncidentList page
            await Shell.Current.GoToAsync("//IncidentList");
        }

    }
}
