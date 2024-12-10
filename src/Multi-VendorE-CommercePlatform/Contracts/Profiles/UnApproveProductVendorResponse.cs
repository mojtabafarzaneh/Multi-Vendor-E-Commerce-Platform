using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Contracts.Profiles;

public class UnApproveProductVendorResponse
{
    public Guid Id { get; set; }
    public string BusinessName { get; set; }
    public bool Approved { get; set; }
    public ICollection<UnApproveProductResponse> Products { get; set; }
    
}