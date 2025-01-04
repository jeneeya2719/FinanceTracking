using Expense_Tracking.Models;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;


namespace Expense_Tracking
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
                });

            builder.Services.AddMauiBlazorWebView();
            //builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<Services.FinanceService>();
            builder.Services.AddSingleton<GlobalState>();


            builder.Services.AddMudServices();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
