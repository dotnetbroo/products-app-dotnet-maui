using Microsoft.AspNetCore.Mvc;
using ProductsApp.Api.Services;
using ProductsApp.Shared.Dtos;

namespace ProductsApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<List<ProductDto>> GetAll()
    {
        var result = _productService.GetAll();
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<ProductDto> Create([FromBody] CreateProductDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest("Product name is required.");

        if (dto.Price < 0)
            return BadRequest("Price cannot be negative.");

        if (dto.Quantity < 0)
            return BadRequest("Quantity cannot be negative.");

        var result = _productService.Add(dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var deleted = _productService.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}