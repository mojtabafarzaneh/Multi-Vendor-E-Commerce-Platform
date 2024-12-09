using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;
using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Services.Implenetations;

public class AuthService: IAuthService
{
    private readonly ICustomerManager _customerManager;
    private readonly ILogger<AuthService> _logger;
    private readonly IAuthManager _authManager;
    private readonly IMapper _mapper;
    private readonly IVendorManager _vendorManager;

    public AuthService(ILogger<AuthService> logger,
        IAuthManager authManager,
        IMapper mapper,
        ICustomerManager customerManager,
        IVendorManager vendorManager)
    {
        _logger = logger;
        _authManager = authManager;
        _mapper = mapper;
        _customerManager = customerManager;
        _vendorManager = vendorManager;
    }

    public async Task<IEnumerable<IdentityError>> CustomerRegistration(CustomerRegistrationRequest request)
    {
        try
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var createUser = new CreateUser
            {
                Email = request.Email,
                Password = request.Password
            };
            var createCustomer = new CreateCustomer
            {
                Address = request.Address,
                FullName = request.FullName,
            };
            var user = _mapper.Map<User>(createUser);
            var customer = _mapper.Map<Customer>(createCustomer);
            if (customer == null)
            {
                throw new ArgumentException("Customer is not populated");
            }
            user.UserName = user.Email;
            var errors = await _authManager.RegisterCustomer(user, request.Password);
            
            var enumerable = errors as IdentityError[] ?? errors.ToArray();
            var identityErrors = enumerable as IdentityError[] ?? enumerable.ToArray();
            if (identityErrors.Any())
            {
                throw new ($"Registration failed: {identityErrors.First().Description}");
            }

            if (identityErrors.Length == 0)
            {
                customer.UserId = user.Id;
                await _customerManager.Create(customer);
            }
            return enumerable;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<IdentityError>> VendorRegistration(VendorRegistrationRequest request)
    {
        try
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var createUser = new CreateUser
            {
                Email = request.Email,
                Password = request.Password
            };
            var createVendor = new CreateVendor
            {
                Address = request.Address,
                BusinessEmail = request.BusinessEmail,
                BusinessPhone = request.BusinessPhone,
                BusinessName = request.BusinessName,
            };
            var user = _mapper.Map<User>(createUser);
            var vendor = _mapper.Map<Vendor>(createVendor);
            user.UserName = user.Email;
            var errors = await _authManager
                .RegisterVendor(user, request.Password);
            
            var enumerable = errors as IdentityError[] ?? errors.ToArray();
            var identityErrors = enumerable as IdentityError[] ?? enumerable.ToArray();
            if (identityErrors.Any())
            {
                throw new ($"Registration failed: {identityErrors.First().Description}");
            }
            
            if (identityErrors.Length == 0)
            {

                if (await _vendorManager.DoesVendorExist(vendor))
                {
                    await _authManager.Remove(user);
                    throw new ArgumentException("Vendor already exists");
                    
                }
                vendor.UserId = user.Id;
                await _vendorManager.Create(vendor);
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
            
            if (user != null && !await _authManager.DoesPasswordValid(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            Debug.Assert(user != null, nameof(user) + " != null");
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

            if (user != null && !await _authManager.DoesTokenExist(request, user))
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }
            
            var token = await _authManager.GenerateAuthenticationToken(user ?? throw new InvalidOperationException());
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