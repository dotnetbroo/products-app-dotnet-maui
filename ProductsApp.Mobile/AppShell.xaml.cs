using ProductsApp.Mobile.Views;

namespace ProductsApp.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
    }
}