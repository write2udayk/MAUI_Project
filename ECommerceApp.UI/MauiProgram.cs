using ECommerceApp.Services.Services;
using ECommerceApp.UI.Views;
using ECommerceApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.UI
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddTransient<ProductViewModel>();
            builder.Services.AddTransient<ProductsPage>();

            return builder.Build();
        }
    }
}
