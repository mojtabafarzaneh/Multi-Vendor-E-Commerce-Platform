using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Multi_VendorE_CommercePlatform.Contracts.Authentication;

public class RegistrationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }
}