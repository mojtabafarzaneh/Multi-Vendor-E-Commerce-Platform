using System.Security.Claims;

namespace Multi_VendorE_CommercePlatform.Helpers;

public class RoleHelper
{
    private readonly IHttpContextAccessor _ContextAccessor;

    public RoleHelper(IHttpContextAccessor contextAccessor)
    {
        _ContextAccessor = contextAccessor;
    }

    public bool IsAdminUser()
    {
        var user = _ContextAccessor.HttpContext?.User;
        if (user is null || !user.Identity.IsAuthenticated)
        {
            return false;
        }
        return user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
    }

    public bool IsVendorUser()
    {
        var user = _ContextAccessor.HttpContext?.User;
        if (user is null || !user.Identity.IsAuthenticated)
        {
            return false;
        }
        return user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Vendor");
    }
}