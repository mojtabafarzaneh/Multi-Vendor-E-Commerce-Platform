using Multi_VendorE_CommercePlatform.Contracts.Project;

namespace Multi_VendorE_CommercePlatform.Contracts.Profiles;

public class VendorResponse
{
    public string BusinessName { get; set; }
    public string BusinessEmail { get; set; }
    public string BusinessPhone { get; set; }
    public string Address { get; set; }
    public ICollection<ProductResponse> Products { get; set; }
    
}