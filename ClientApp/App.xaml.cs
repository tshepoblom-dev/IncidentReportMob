using ClientApp.Services;

namespace ClientApp
{
    public partial class App : Application
    {
        private readonly IApiService _apiService;
        public App(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            MainPage = new AppShell();
        }
        protected override async void OnStart()
        {
            base.OnStart();
            await CheckAuthTokenAsync();
        }
        protected override async void OnResume()
        {
            base.OnResume();
            await CheckAuthTokenAsync();
        }

        private async Task CheckAuthTokenAsync()
        {
            try
            {
                var token = await _apiService.GetTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    _apiService.SetBearerToken(token);
                    bool isValid = await _apiService.ValidateTokenAsync(token);
                    if (isValid)
                    {
                        await Shell.Current.GoToAsync("//Dashboard");
                        return;
                    }
                    else
                    {
                        await Shell.Current.GoToAsync("//MainPage");
                    }
                }
                else
                {
                    await Shell.Current.GoToAsync("//MainPage");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them)
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
    }
}
