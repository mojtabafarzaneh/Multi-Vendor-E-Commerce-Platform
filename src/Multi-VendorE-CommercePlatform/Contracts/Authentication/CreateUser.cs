using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Multi_VendorE_CommercePlatform.Contracts.Authentication;

public class CreateUser
{
    public string Email { get; set; }
    public string Password { get; set; }
    
}