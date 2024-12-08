using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Services.Implenetations;

public class AuthService: IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IAuthManager _authManager;
    private readonly IMapper _mapper;

    public AuthService(ILogger<AuthService> logger, IAuthManager authManager, IMapper mapper)
    {
        _logger = logger;
        _authManager = authManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IdentityError>> Registration(RegistrationRequest request)
    {
        try
        {
            var user = _mapper.Map<User>(request);
            user.UserName = request.Email;
            var errors = await _authManager.Register(user, request.Password);
            
            var enumerable = errors as IdentityError[] ?? errors.ToArray();
            var identityErrors = enumerable as IdentityError[] ?? enumerable.ToArray();
            if (identityErrors.Any())
            {
                throw new ($"Registration failed: {identityErrors.First().Description}");
            }

            return enumerable;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<AuthUserResponse> Login(UserLoginRequest request)
    {
        try
        {
            var user = await _authManager.DoesUserExist(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            if (!await _authManager.DoesPasswordValid(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var token = await _authManager.GenerateAuthenticationToken(user);
            var refreshToken = await _authManager.GenerateAuthenticationRefreshToken(user);
            return new AuthUserResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                UserId = user.Id
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<AuthUserResponse> RefreshToken(AuthUserResponse request)
    {
        try
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _logger.LogInformation($"{request.Token}");
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);

            
            var email = tokenContent.Claims
                .FirstOrDefault(x => string.Equals(x.Type, "email", StringComparison.OrdinalIgnoreCase))
                ?.Value;
            if (email == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }
            var user = await _authManager.DoesUserExist(email);
            if (user == null)
            {
                _logger.LogWarning($"No user found with email: {email}");
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            if (!await _authManager.DoesTokenExist(request, user))
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }
            
            var token = await _authManager.GenerateAuthenticationToken(user);
            var refreshToken = await _authManager.GenerateAuthenticationRefreshToken(user);
                
            return new AuthUserResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                UserId = user.Id
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to verify refresh token: {ex.Message}");
            throw;
        }
    }
}