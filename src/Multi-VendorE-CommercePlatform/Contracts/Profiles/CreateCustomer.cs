using System.ComponentModel.DataAnnotations;

namespace Multi_VendorE_CommercePlatform.Contracts.Profiles;

public class CreateCustomer
{
    public string FullName { get; set; }
    public string Address { get; set; }
}