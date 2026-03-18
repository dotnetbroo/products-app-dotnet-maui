using ProductsApp.Mobile.Services;
using ProductsApp.Mobile.ViewModels;
using ProductsApp.Mobile.Views;

namespace ProductsApp.Mobile;

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

#if ANDROID
        string baseUrl = "https://localhost:7053/";
#else
        string baseUrl = "https://localhost:7053/";
#endif

        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        });

        builder.Services.AddSingleton<ProductApiService>();

        builder.Services.AddSingleton<ProductListViewModel>();
        builder.Services.AddTransient<AddProductViewModel>();

        builder.Services.AddSingleton<ProductListPage>();
        builder.Services.AddTransient<AddProductPage>();

        return builder.Build();
    }
}