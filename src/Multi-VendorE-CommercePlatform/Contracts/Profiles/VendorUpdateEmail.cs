using System.ComponentModel.DataAnnotations;

namespace Multi_VendorE_CommercePlatform.Contracts.Profiles;

public class VendorUpdateEmail
{
    
    [Required]
    [EmailAddress]
    public string BusinessEmail { get; set; }
    
}