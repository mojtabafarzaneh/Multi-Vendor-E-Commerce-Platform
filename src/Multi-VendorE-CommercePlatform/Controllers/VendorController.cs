using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Controllers;
[ApiController]
public class VendorController: ControllerBase
{
    private readonly IVendorService _vendorService;

    public VendorController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [HttpGet(ApiEndpoints.Vendor.Me)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetVendor()
    {
        var response = await _vendorService.GetVendorByIdAsync();
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Vendor.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> UpdateEmailVendor([FromBody] VendorUpdateEmail request)
    {
        await _vendorService.UpdateVendorBusinessAsync(request);
        return Ok();
    }

    [HttpDelete(ApiEndpoints.Vendor.Me)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> DeleteVendor()
    {
        await _vendorService.DeleteVendorAsync();
        return Ok();
    }
    
}