using System.Windows.Input;
using ProductsApp.Mobile.Services;
using ProductsApp.Shared.Dtos;

namespace ProductsApp.Mobile.ViewModels;

public class AddProductViewModel : BaseViewModel
{
    private readonly ProductApiService _productApiService;

    private string _name = string.Empty;
    private decimal _price;
    private int _quantity;

    public ICommand SaveCommand { get; }

    public AddProductViewModel(ProductApiService productApiService)
    {
        _productApiService = productApiService;
        Title = "Add Product";
        SaveCommand = new Command(async () => await SaveAsync());
    }

    public string Name
    {
        get => _name;
        set
        {
            if (_name == value) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (_price == value) return;
            _price = value;
            OnPropertyChanged();
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (_quantity == value) return;
            _quantity = value;
            OnPropertyChanged();
        }
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            await Shell.Current.DisplayAlert("Validation", "Name is required", "OK");
            return;
        }

        if (Price < 0 || Quantity < 0)
        {
            await Shell.Current.DisplayAlert("Validation", "Price or Quantity invalid", "OK");
            return;
        }

        var dto = new CreateProductDto
        {
            Name = Name,
            Price = Price,
            Quantity = Quantity
        };

        var result = await _productApiService.AddProductAsync(dto);

        if (result is null)
        {
            await Shell.Current.DisplayAlert("Error", "Product was not added", "OK");
            return;
        }

        await Shell.Current.DisplayAlert("Success", "Product added", "OK");
        await Shell.Current.GoToAsync("..");
    }
}