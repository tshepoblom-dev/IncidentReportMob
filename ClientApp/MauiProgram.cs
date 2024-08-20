using ClientApp.Models;
using ClientApp.Services;
using ClientApp.Viewmodels;
using ClientApp.Views;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace ClientApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            var httpClient = new HttpClient { BaseAddress = new Uri(Constants.URL) };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            builder.Services.AddSingleton<HttpClient>(httpClient);
            builder.Services.AddSingleton<IRemoteApiService, RemoteApiService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageVM>();

            builder.Services.AddSingleton<IncidentList>();
            builder.Services.AddSingleton<IncidentListViewmodel>();
            builder.Services.AddTransient<IncidentDetails>();
            builder.Services.AddTransient<IncidentDetailsViewmodel>();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
