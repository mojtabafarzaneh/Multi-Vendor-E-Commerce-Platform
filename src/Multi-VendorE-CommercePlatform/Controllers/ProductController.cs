using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Controllers;

[ApiController]
public class ProductController : ControllerBase
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
    public async Task<IActionResult> GetAllProducts([FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string search = null!,
        [FromQuery] bool isApproved = true)
    {
        var response = await _productService
            .GetAllProducts(page, pageSize, search, isApproved);
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Product.GetById)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id)
    {
        var response = await _productService.GetProductById(id);
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Product.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id,
        [FromBody] UpdateProductNameAndDescription product)
    {
        if (id == Guid.Empty || product.Description == null! || product.Name == null!)
        {
            return BadRequest("invalid data provided");
        }
        await _productService.UpdateNameAndDescription(product, id);
        return Ok();
    }

    [HttpDelete(ApiEndpoints.Product.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("invalid data provided");
        }

        await _productService.RemoveProduct(id);
        return Ok();
    }
}