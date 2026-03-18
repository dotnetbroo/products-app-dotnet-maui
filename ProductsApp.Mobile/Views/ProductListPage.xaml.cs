using ProductsApp.Mobile.ViewModels;

namespace ProductsApp.Mobile.Views;

public partial class ProductListPage : ContentPage
{
    private readonly ProductListViewModel _viewModel;

    public ProductListPage(ProductListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel.Products.Count == 0)
            await _viewModel.LoadProductsAsync();
    }
}