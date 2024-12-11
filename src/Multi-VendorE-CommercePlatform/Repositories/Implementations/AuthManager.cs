using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Models.Entities;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Multi_VendorE_CommercePlatform.Repositories.Implementations;

public class AuthManager : IAuthManager
{
    private readonly IConfiguration _config;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AuthManager> _logger;
    private readonly UserManager<User> _userManager;

    public AuthManager(UserManager<User> userManager,
        ILogger<AuthManager> logger,
        IConfiguration config, ApplicationDbContext context)
    {
        _userManager = userManager;
        _logger = logger;
        _config = config;
        _context = context;
    }

    public async Task<IEnumerable<IdentityError>> RegisterCustomer(User request, string password)
    {
        var result = await _userManager.CreateAsync(request, password);
        if (result.Succeeded) await _userManager.AddToRoleAsync(request, "Customer");
        return result.Errors;
    }

    public async Task<IEnumerable<IdentityError>> RegisterVendor(User request, string password)
    {
        var result = await _userManager.CreateAsync(request, password);
        if (result.Succeeded) await _userManager.AddToRoleAsync(request, "Vendor");
        return result.Errors;
    }

    public async Task<string> GenerateAuthenticationRefreshToken(User user)
    {
        var refreshToken = await GenerateRefreshToken(user);

        return refreshToken;
    }

    public async Task<string> GenerateAuthenticationToken(User user)
    {
        var token = await GenerateJwtToken(user);

        return token;
    }

    public async Task Remove(User user)
    {
        try
        {
            await _userManager.DeleteAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> DoesTokenExist(AuthUserResponse request, User user)
    {
        return await _userManager.VerifyUserTokenAsync(
            user, "MultiVendorECommerceApi",
            "RefreshToken", request.RefreshToken);
    }

    public async Task<User?> DoesUserExist(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            _logger.LogWarning($"No user found with email: {email}");
            return null;
        }

        return user;
    }

    public async Task<bool> DoesPasswordValid(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);

        if (user.Email != null)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new("uid", user.Id.ToString())
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_config["Jwt:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        return null!;
    }

    private async Task<string> GenerateRefreshToken(User user)
    {
        await _userManager.RemoveAuthenticationTokenAsync(
            user, "MultiVendorECommerceApi", "RefreshToken");

        var newRefreshToken = await _userManager.GenerateUserTokenAsync(
            user, "MultiVendorECommerceApi", "RefreshToken");
        await _userManager.SetAuthenticationTokenAsync(
            user, "MultiVendorECommerceApi", "RefreshToken", newRefreshToken);

        return newRefreshToken;
    }
}