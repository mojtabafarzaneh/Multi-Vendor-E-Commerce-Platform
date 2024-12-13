using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Controllers;

[ApiController]
public class ProductController:ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost(ApiEndpoints.Product.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult> CreateProduct([FromBody] ProductRequest product)
    {
        await _productService.AddProduct(product);
        return Ok();
    }

    [HttpGet(ApiEndpoints.Product.GetAll)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetAllProducts([FromQuery] int page =1 ,
        [FromQuery] int pageSize = 10,
        [FromQuery] string search = null!)
    {
        var response = await _productService
            .GetAllProducts(page, pageSize, search);
        return Ok(response);
    }
}