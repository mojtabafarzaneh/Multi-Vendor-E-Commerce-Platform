using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface IAdminService
{
    public Task<List<UnApproveVendorResponse>> GetAllUnapprovedVendors();
    public Task ApproveVendors(Guid id);
    public Task<ICollection<UnApproveProductResponse>> GetAllUnapprovedProducts();
    public Task ApproveProducts(Guid id);
}