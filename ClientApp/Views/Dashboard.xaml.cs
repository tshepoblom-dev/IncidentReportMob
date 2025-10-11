using ClientApp.Viewmodels;

namespace ClientApp.Views;

public partial class Dashboard : ContentPage
{
	public Dashboard(DashboardViewmodel homePageViewmodel)
	{
		BindingContext = homePageViewmodel;
		InitializeComponent();
	}
}