using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using TestMobile.Handler;
using TestMobile.Pages.Vehicles;
using TestMobile.Services;

namespace TestMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            Environment.SetEnvironmentVariable("API_BASE_URI", "http://10.0.2.2:5249/api/");

            builder.Services.AddHttpClient<AuthService>();
            builder.Services.AddHttpClient<VehicleService>()
             .AddHttpMessageHandler<AuthTokenHandler>();

            builder.Services.AddTransient<VehiclesInit>();
            builder.Services.AddTransient<AuthTokenHandler>();
      

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
