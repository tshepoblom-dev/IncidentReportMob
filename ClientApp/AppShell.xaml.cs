using ClientApp.Views;

namespace ClientApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(IncidentDetails), typeof(IncidentDetails));
            Routing.RegisterRoute(nameof(IncidentList), typeof(IncidentList));
            Routing.RegisterRoute(nameof(HouseholdList), typeof(HouseholdList));
            Routing.RegisterRoute(nameof(HouseholdDetailsPage), typeof(HouseholdDetailsPage));
            Routing.RegisterRoute(nameof(AddHouseholdPage), typeof(AddHouseholdPage));
        }
    }
}
