using ProductsApp.Shared.Dtos;

namespace ProductsApp.Api.Services;

public class ProductService : IProductService
{
    private readonly List<ProductDto> _products =
    [
        new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "iPhone 15 Pro",
            Price = 1450,
            Quantity = 6
        },
        new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "Samsung S24 Ultra",
            Price = 1320,
            Quantity = 4
        },
        new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "AirPods Pro 2",
            Price = 280,
            Quantity = 15
        },
        new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = "Xiaomi Pad 6",
            Price = 420,
            Quantity = 8
        }
    ];

    public List<ProductDto> GetAll()
    {
        return _products
            .OrderByDescending(x => x.Price)
            .ToList();
    }

    public ProductDto Add(CreateProductDto dto)
    {
        var product = new ProductDto
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity
        };

        _products.Add(product);
        return product;
    }

    public bool Delete(Guid id)
    {
        var item = _products.FirstOrDefault(x => x.Id == id);
        if (item is null)
            return false;

        _products.Remove(item);
        return true;
    }
}