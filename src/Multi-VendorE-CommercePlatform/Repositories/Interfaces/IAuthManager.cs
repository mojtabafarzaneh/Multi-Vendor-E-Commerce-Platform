using Microsoft.AspNetCore.Identity;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface IAuthManager
{
    public Task<IEnumerable<IdentityError>> RegisterCustomer(User request, string password);
    public Task<IEnumerable<IdentityError>> RegisterVendor(User request, string password);
    public Task<string> GenerateAuthenticationToken(User user);
    public Task Remove(User user);
    public Task<User?> DoesUserExist(string email);
    public Task<bool> DoesPasswordValid(User user, string password);
    public Task<string> GenerateAuthenticationRefreshToken(User user);

    public Task<bool> DoesTokenExist(AuthUserResponse request, User user);
}