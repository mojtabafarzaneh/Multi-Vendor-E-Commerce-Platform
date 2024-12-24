using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Controllers;

[ApiController]
public class CustomerController:ControllerBase
{
    private readonly ICustomerService _customer;

    public CustomerController(ICustomerService customer)
    {
        _customer = customer;
    }

    [HttpGet(ApiEndpoints.Profile.ProfilePage)]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var response = await _customer.Profile();
        return Ok(response);
    }
}