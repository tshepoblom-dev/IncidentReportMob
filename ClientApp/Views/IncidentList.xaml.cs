using ClientApp.Viewmodels;

namespace ClientApp.Views;

public partial class IncidentList : ContentPage
{
	public IncidentList(IncidentListViewmodel incidentListViewmodel)
	{
		BindingContext = incidentListViewmodel;
		InitializeComponent();
	}
}