using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Controllers;

[ApiController]
public class CategoryController: ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet(ApiEndpoints.Category.GetById)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> GetCategoryByIdAsync([FromRoute]Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }
    
    [HttpGet(ApiEndpoints.Category.GetAll)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> GetCategoriesAsync()
    {
        var category = await _categoryService.GetCategoriesAsync();
        return Ok(category);
    }

    [HttpPost(ApiEndpoints.Category.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> CreateCategoryAsync([FromBody]CategoryRequest category)
    {
        await _categoryService.Create(category);
        return Ok();
    }

    [HttpPut(ApiEndpoints.Category.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryRequest category, [FromRoute] Guid id)
    {
        await _categoryService.Update(category, id);
        return Ok();
    }

    [HttpDelete(ApiEndpoints.Category.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> DeleteCategoryAsync([FromRoute] Guid id)
    {
        await _categoryService.Delete(id);
        return Ok();
    }
    

}