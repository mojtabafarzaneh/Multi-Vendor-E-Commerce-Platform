using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Controllers;

[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet(ApiEndpoints.Admin.UnApproveVendors)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UnApproveVendors(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string search = null!)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page and PageSize must be greater than 0");
        }
        var response = await _adminService
            .GetAllUnapprovedVendors(page, pageSize, search);
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Admin.ChangeVendorStatus)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> ChangeVendorStatus([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("provide a valid id");
        }
        await _adminService.ApproveVendors(id);
        return Ok();
    }

    [HttpGet(ApiEndpoints.Admin.UnApproveProducts)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UnApproveProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string search = null!)
    {
        if (page < 1 || pageSize < 1)
        {
            return BadRequest("Page and PageSize must be greater than 0");
        }
        var response = await _adminService
            .GetAllUnapprovedProducts(page, pageSize, search);
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Admin.ChangeProductStatus)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> ChangeProductStatus([FromRoute] Guid id)
    {
        await _adminService.ApproveProducts(id);
        return Ok();
    }
}