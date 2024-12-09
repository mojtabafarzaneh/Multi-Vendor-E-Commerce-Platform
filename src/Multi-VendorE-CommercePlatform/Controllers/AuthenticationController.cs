using Microsoft.AspNetCore.Mvc;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Controllers;

[ApiController]
public class AuthenticationController: ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost(ApiEndpoints.Registration.Signup)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] CustomerRegistrationRequest request)
    {
        await _authService.CustomerRegistration(request);
        return Ok();
    }

    [HttpPost(ApiEndpoints.Registration.Login)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody]  UserLoginRequest request)
    {
        var response = await _authService.Login(request);
        return Ok(response);
    }

    [HttpPost(ApiEndpoints.Registration.RefreshToken)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshToken([FromBody] AuthUserResponse request)
    {
        var response = await _authService.RefreshToken(request);
        return Ok(response);
    }
    
    [HttpPost(ApiEndpoints.Registration.VendorRegister)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> VendorRegister([FromBody] VendorRegistrationRequest request)
    {
        await _authService.VendorRegistration(request);
        return Ok();
    }
}