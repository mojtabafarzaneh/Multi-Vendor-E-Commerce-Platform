using Microsoft.AspNetCore.Identity;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface IAuthService
{
    public Task<IEnumerable<IdentityError>> CustomerRegistration(CustomerRegistrationRequest request);
    public Task<IEnumerable<IdentityError>> VendorRegistration(VendorRegistrationRequest request);
    public Task<AuthUserResponse> Login(UserLoginRequest request);
    public Task<AuthUserResponse> RefreshToken(AuthUserResponse request);
}