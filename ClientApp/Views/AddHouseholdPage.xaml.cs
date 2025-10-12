using ClientApp.Viewmodels;
namespace ClientApp.Views;

public partial class AddHouseholdPage : ContentPage
{
    private readonly View[] _steps;
    private int _currentIndex = 0;

    public AddHouseholdPage(AddHouseholdViewmodel addHouseholdViewmodel)
	{
        BindingContext = addHouseholdViewmodel;
		InitializeComponent();
/*
        _steps = new View[]
        {
            new HouseholdProfileStep(),
            new MembersStep(),
            new MediaStep(),
            new ServicesStep(),
            new ReviewAndSaveStep()
        };
        StepContent.Content = _steps[_currentIndex];*/
    }
    /*
    private async Task AnimateStepAsync(int newIndex)
    {
        if (newIndex < 0 || newIndex >= _steps.Length)
            return;

        var oldStep = StepContent.Content;
        var newStep = _steps[newIndex];

        await oldStep.FadeTo(0, 150);
        StepContent.Content = newStep;
        newStep.Opacity = 0;
        await newStep.FadeTo(1, 200);
    }*/
}