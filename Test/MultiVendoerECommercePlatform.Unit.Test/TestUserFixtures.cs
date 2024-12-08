using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace TestProject1;

public class TestUserFixtures
{
    public ClaimsPrincipal CreateUser(string userId, string email, string roles)
    {
        var claims = new List<Claim>
        {
            new Claim("uid", userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, "Test User"),
            new Claim(ClaimTypes.Role, roles)
        };
        

        var identity = new ClaimsIdentity(claims, "TestAuthType");
        return new ClaimsPrincipal(identity);
    }

    public IHttpContextAccessor CreateHttpContextAccessor(string userId, string email, string roles)
    {
        var contextAccessor = Substitute.For<IHttpContextAccessor>();
        var user = CreateUser(userId, email, roles);
        var httpContext = new DefaultHttpContext { User = user };
        contextAccessor.HttpContext.Returns(httpContext);

        return contextAccessor;
    }
}