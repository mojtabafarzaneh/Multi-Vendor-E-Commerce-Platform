using Microsoft.AspNetCore.Identity;

namespace Multi_VendorE_CommercePlatform.Models;


public class UserRoles: IdentityRole<Guid>
{
    public UserRoles()
    { 
        Id = Guid.NewGuid();
    }
}
