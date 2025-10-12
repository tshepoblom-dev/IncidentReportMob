using ClientApp.Viewmodels;

namespace ClientApp.Views;

public partial class HouseholdDetailsPage : ContentPage
{
	public HouseholdDetailsPage(HouseholdDetailViewModel householdDetailViewModel)
	{
		InitializeComponent();
		BindingContext = householdDetailViewModel;
	}
}