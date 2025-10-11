using ClientApp.Viewmodels;

namespace ClientApp.Views;

public partial class AddIncidentPage : ContentPage
{
	public AddIncidentPage(AddIncidentViewmodel vm)
	{
		BindingContext = vm;
		InitializeComponent();
		// Subscribe to StepChanged if you raise an event or bind to Step property changes
		this.BindingContextChanged += AddIncidentPage_BindingContextChanged;
	}

    private void AddIncidentPage_BindingContextChanged(object? sender, EventArgs e)
    {
        if (BindingContext is Viewmodels.AddIncidentViewmodel vm)
        {
            vm.OnStepChanged += async (oldStep, newStep) => await AnimateStepChangeAsync(oldStep, newStep);
        }
    }

    private async Task AnimateStepChangeAsync(int oldStep, int newStep)
    {
        View oldView = GetStepView(oldStep);
        View newView = GetStepView(newStep);

        if (oldView == null || newView == null)
            return;

        // Fade out old step
        await oldView.FadeTo(0, 150, Easing.CubicIn);
        oldView.IsVisible = false;

        // Prepare new step (start invisible and slide in)
        newView.Opacity = 0;
        newView.TranslationX = newStep > oldStep ? 100 : -100;
        newView.IsVisible = true;

        // Animate in
        await Task.WhenAll(
            newView.FadeTo(1, 250, Easing.CubicOut),
            newView.TranslateTo(0, 0, 250, Easing.CubicOut)
        );
    }

    private View? GetStepView(int step)
    {
        return step switch
        {
            0 => Step1Frame,
            1 => Step2Frame,
            2 => Step3Frame,
            3 => Step4Frame,
            _ => null
        };
    }
}