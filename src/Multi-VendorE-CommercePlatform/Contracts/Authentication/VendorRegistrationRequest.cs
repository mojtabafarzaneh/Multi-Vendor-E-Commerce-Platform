using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Multi_VendorE_CommercePlatform.Contracts.Authentication;

public class VendorRegistrationRequest
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] [PasswordPropertyText] public string Password { get; set; }

    [Required] public string BusinessName { get; set; }

    [Required] [EmailAddress] public string BusinessEmail { get; set; }

    [Required] public string BusinessPhone { get; set; }

    [Required] public string Address { get; set; }
}