using System.Collections.ObjectModel;
using System.Windows.Input;
using ProductsApp.Mobile.Services;
using ProductsApp.Shared.Dtos;

namespace ProductsApp.Mobile.ViewModels;

public class ProductListViewModel : BaseViewModel
{
    private readonly ProductApiService _productApiService;

    public ObservableCollection<ProductDto> Products { get; set; } = [];

    public ICommand LoadProductsCommand { get; }
    public ICommand GoToAddPageCommand { get; }
    public ICommand DeleteProductCommand { get; }

    public ProductListViewModel(ProductApiService productApiService)
    {
        _productApiService = productApiService;

        Title = "Products";
        LoadProductsCommand = new Command(async () => await LoadProductsAsync());
        GoToAddPageCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(Views.AddProductPage)));
        DeleteProductCommand = new Command<ProductDto>(async product => await DeleteProductAsync(product));
    }

    public async Task LoadProductsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            Products.Clear();

            var items = await _productApiService.GetProductsAsync();

            foreach (var item in items)
                Products.Add(item);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task DeleteProductAsync(ProductDto? product)
    {
        if (product is null)
            return;

        var confirm = await Shell.Current.DisplayAlert(
            "Delete",
            $"Delete {product.Name}?",
            "Yes",
            "No");

        if (!confirm)
            return;

        var deleted = await _productApiService.DeleteProductAsync(product.Id);

        if (deleted)
            Products.Remove(product);
    }
}