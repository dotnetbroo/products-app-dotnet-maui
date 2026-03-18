using System.Net.Http.Json;
using ProductsApp.Shared.Dtos;

namespace ProductsApp.Mobile.Services;

public class ProductApiService
{
    private readonly HttpClient _httpClient;

    public ProductApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductDto>> GetProductsAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/products");
        return result ?? [];
    }

    public async Task<ProductDto?> AddProductAsync(CreateProductDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/products", dto);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ProductDto>();
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/products/{id}");
        return response.IsSuccessStatusCode;
    }
}