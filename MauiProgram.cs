﻿using Firebase.Auth.Providers;
using Firebase.Auth;
using LocalBusinessExplorer.Services;
using LocalBusinessExplorer.ViewModel;
using LocalBusinessExplorer.Views;
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

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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

            builder.Services.AddSingleton<FirebaseService>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();    


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
