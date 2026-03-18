using ProductsApp.Shared.Dtos;

namespace ProductsApp.Api.Services;

public interface IProductService
{
    List<ProductDto> GetAll();
    ProductDto Add(CreateProductDto dto);
    bool Delete(Guid id);
}