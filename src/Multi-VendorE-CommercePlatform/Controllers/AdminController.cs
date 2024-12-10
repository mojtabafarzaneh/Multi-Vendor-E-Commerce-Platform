using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Controllers;

[ApiController]
public class AdminController: ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet(ApiEndpoints.Admin.UnApproveVendors)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UnApproveVendors()
    {
        var response = await _adminService.GetAllUnapprovedVendors();
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Admin.ChangeVendorStatus)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> ChangeVendorStatus([FromRoute] Guid id)
    {
        await _adminService.ApproveVendors(id);
        return Ok();
    }

    [HttpGet(ApiEndpoints.Admin.UnApproveProducts)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UnApproveProducts()
    {
        var response = await _adminService.GetAllUnapprovedProducts();
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