using ClientApp.Viewmodels;

namespace ClientApp.Views;

public partial class IncidentDetails : ContentPage
{
	public IncidentDetails(IncidentDetailsViewmodel incidentDetailsViewmodel)
	{
		BindingContext = incidentDetailsViewmodel;
		InitializeComponent();
	}
}