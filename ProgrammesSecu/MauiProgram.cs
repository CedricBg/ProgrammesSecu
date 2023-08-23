using Microsoft.Extensions.Logging;
using ProgrammesSecu.Helpers;
using ProgrammesSecu.Services;
using ProgrammesSecu.ViewModels;
using ProgrammesSecu.Views;

namespace ProgrammesSecu
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<NfcPage>();
            builder.Services.AddSingleton<DashboardPage>();
            builder.Services.AddSingleton<ServerPage>();

            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<NfcViewModel>();
            builder.Services.AddSingleton<DashboardViewModel>();
            builder.Services.AddSingleton<ServerPageViewModel>();

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<DataAccesServices>();
            builder.Services.AddSingleton<ConnectivityServices>();
            builder.Services.AddSingleton<AuthServices>();
            builder.Services.AddSingleton<BearerToken>();



        
            

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}