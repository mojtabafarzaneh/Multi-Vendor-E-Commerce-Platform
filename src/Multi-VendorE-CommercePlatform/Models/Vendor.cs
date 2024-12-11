using System.ComponentModel.DataAnnotations;

namespace Multi_VendorE_CommercePlatform.Models;

public class Vendor
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string BusinessName { get; set; }

    [EmailAddress] public string BusinessEmail { get; set; }

    [Phone] public string BusinessPhone { get; set; }

    public string Address { get; set; }
    public bool Approved { get; set; } = false;


    public ICollection<Product> Products { get; set; }
}