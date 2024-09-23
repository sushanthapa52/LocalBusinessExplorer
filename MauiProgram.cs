using LocalBusinessExplorer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LocalBusinessExplorer
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

            // Add appsettings.json configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)  // Or use any path where your appsettings.json is stored
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            builder.Services.AddSingleton<FirebaseService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
