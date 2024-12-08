using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Implenetations;
using NSubstitute;

namespace TestProject1;

public class AuthServiceTests
{
    private readonly ILogger<AuthService> _mockLogger;
    private readonly IAuthManager _mockAuthManager;
    private readonly IMapper _mockMapper;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _mockLogger = Substitute.For<ILogger<AuthService>>();
        _mockAuthManager = Substitute.For<IAuthManager>();
        _mockMapper = Substitute.For<IMapper>();
        _authService = new AuthService(_mockLogger, _mockAuthManager, _mockMapper);
    }

    #region Registration Tests
    [Fact]
    public async Task Registration_ValidRequest_ShouldSucceed()
    {
        // Arrange
        var request = new RegistrationRequest 
        { 
            Email = "test@example.com", 
            Password = "StrongPassword123!" 
        };
        var user = new User { UserName = request.Email };

        _mockMapper.Map<User>(request).Returns(user);
        _mockAuthManager.Register(user, request.Password).Returns(Enumerable.Empty<IdentityError>());

        // Act
        var result = await _authService.Registration(request);

        // Assert
        result.Should().BeEmpty();
        await _mockAuthManager.Received(1).Register(user, request.Password);
        _mockMapper.Received(1).Map<User>(request);
    }

    [Fact]
    public async Task Registration_FailedRegistration_ShouldThrowException()
    {
        // Arrange
        var request = new RegistrationRequest 
        { 
            Email = "test@example.com", 
            Password = "StrongPassword123!" 
        };
        var user = new User { UserName = request.Email };
        var identityErrors = new[]
        {
            new IdentityError { Description = "Password too weak" }
        };

        _mockMapper.Map<User>(request).Returns(user);
        _mockAuthManager.Register(user, request.Password).Returns(identityErrors);

        // Act
        Func<Task> act = async () => await _authService.Registration(request);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Registration failed: Password too weak");
        _mockLogger.Received(1);
    }
    #endregion

    #region Login Tests
    [Fact]
    public async Task Login_ValidCredentials_ShouldReturnAuthUserResponse()
    {
        // Arrange
        var request = new UserLoginRequest 
        { 
            Email = "test@example.com", 
            Password = "ValidPassword123!" 
        };
        var user = new User 
        { 
            Id = Guid.NewGuid(), 
            Email = request.Email 
        };
        const string token = "validToken";
        const string refreshToken = "validRefreshToken";

        _mockAuthManager.DoesUserExist(request.Email).Returns(user);
        _mockAuthManager.DoesPasswordValid(user, request.Password).Returns(true);
        _mockAuthManager.GenerateAuthenticationToken(user).Returns(token);
        _mockAuthManager.GenerateAuthenticationRefreshToken(user).Returns(refreshToken);

        // Act
        var result = await _authService.Login(request);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(token);
        result.RefreshToken.Should().Be(refreshToken);
        result.UserId.Should().Be(user.Id);

        await _mockAuthManager.Received(1).DoesUserExist(request.Email);
        await _mockAuthManager.Received(1).DoesPasswordValid(user, request.Password);
    }
    

    [Fact]
    public async Task Login_InvalidPassword_ShouldThrowUnauthorizedException()
    {
        // Arrange
        var request = new UserLoginRequest 
        { 
            Email = "test@example.com", 
            Password = "InvalidPassword" 
        };
        var user = new User 
        { 
            Id = Guid.NewGuid(), 
            Email = request.Email 
        };

        _mockAuthManager.DoesUserExist(request.Email).Returns(user);
        _mockAuthManager.DoesPasswordValid(user, request.Password).Returns(false);

        // Act
        Func<Task> act = async () => await _authService.Login(request);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Invalid username or password");
    }
    #endregion

    #region RefreshToken Tests
    [Fact]
    public async Task RefreshToken_ValidRequest_ShouldReturnNewTokens()
    {
        // Arrange
        var existingTokenResponse = new AuthUserResponse
        {
            Token = CreateTestJwtToken("test@example.com"),
            RefreshToken = "oldRefreshToken",
            UserId = Guid.NewGuid()
        };

        var user = new User 
        { 
            Id = Guid.NewGuid(), 
            Email = "test@example.com" 
        };
        const string newToken = "newToken";
        const string newRefreshToken = "newRefreshToken";

        _mockAuthManager.DoesUserExist("test@example.com").Returns(user);
        _mockAuthManager.DoesTokenExist(existingTokenResponse, user).Returns(true);
        _mockAuthManager.GenerateAuthenticationToken(user).Returns(newToken);
        _mockAuthManager.GenerateAuthenticationRefreshToken(user).Returns(newRefreshToken);

        // Act
        var result = await _authService.RefreshToken(existingTokenResponse);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(newToken);
        result.RefreshToken.Should().Be(newRefreshToken);
        result.UserId.Should().Be(user.Id);
    }
    

    [Fact]
    public async Task RefreshToken_InvalidRefreshToken_ShouldThrowUnauthorizedException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var existingTokenResponse = new AuthUserResponse
        {
            Token = CreateTestJwtToken("test@example.com"),
            RefreshToken = "invalidRefreshToken",
            UserId = userId
        };

        var user = new User 
        { 
            Id = userId, 
            Email = "test@example.com" 
        };

        _mockAuthManager.DoesUserExist("test@example.com").Returns(user);
        _mockAuthManager.DoesTokenExist(existingTokenResponse, user).Returns(false);

        // Act
        Func<Task> act = async () => await _authService.RefreshToken(existingTokenResponse);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage("Invalid refresh token");
    }
    #endregion

    // Helper method to create a test JWT token with email claim
    private string CreateTestJwtToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new[]
        {
            new Claim("email", email)
        };
        var token = new JwtSecurityToken(
            claims: claims
        );
        return tokenHandler.WriteToken(token);
    }
}