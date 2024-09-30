using Firebase.Auth.Providers;
using Firebase.Auth;
using LocalBusinessExplorer.Services;
using LocalBusinessExplorer.ViewModel;
using LocalBusinessExplorer.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

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
            var assembly = typeof(App).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream("LocalBusinessExplorer.appsettings.json"))
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
                var firebaseConfig = config.GetSection("Firebase");
                builder.Services.AddSingleton(services => new FirebaseAuthClient(new FirebaseAuthConfig()
                {
                    ApiKey = firebaseConfig["ApiKey"],
                    AuthDomain = firebaseConfig["AuthDomain"],
                    Providers = new FirebaseAuthProvider[]
                    {
                    new EmailProvider()
                    },
                }));

                // Load Google configuration
                var googleConfig = config.GetSection("Google");
                builder.Services.AddSingleton(googleConfig);
            }

            builder.Services.AddSingleton<FirebaseService>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<SignUpPage>();
            builder.Services.AddTransient<BusinessListingsPage>();
            builder.Services.AddTransient<SignUpViewModel>();
            
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<BusinessListingsViewModel>();
            builder.Services.AddTransient<GooglePlaceAPI>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
