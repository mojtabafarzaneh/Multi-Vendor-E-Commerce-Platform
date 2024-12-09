using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Multi_VendorE_CommercePlatform.Contracts.Profiles;

namespace Multi_VendorE_CommercePlatform.Contracts.Authentication;

public class RegistrationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
    [Required] 
    public string FullName { get; set; }
    [Required] 
    public string Address { get; set; }

}