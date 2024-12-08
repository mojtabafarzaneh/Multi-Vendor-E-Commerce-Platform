using Microsoft.AspNetCore.Identity;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface IAuthService
{
    public Task<IEnumerable<IdentityError>> Registration(RegistrationRequest request);
    public Task<AuthUserResponse> Login(UserLoginRequest request);
    public Task<AuthUserResponse> RefreshToken(AuthUserResponse request);
}