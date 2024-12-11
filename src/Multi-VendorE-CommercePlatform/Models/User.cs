using Microsoft.AspNetCore.Identity;

namespace Multi_VendorE_CommercePlatform.Models;

public class User : IdentityUser<Guid>
{
    public User()
    {
        Id = Guid.NewGuid();
    }

    public Customer Customer { get; set; }
    public Vendor Vendor { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}