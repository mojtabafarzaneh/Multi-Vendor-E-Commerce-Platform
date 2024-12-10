using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface IAdminManager
{
    public Task<bool> DoesAdminExist(Guid id);
    public Task<List<Vendor>> UnapprovedVendors();
    public Task ChangeApprovedVendorsStatus(Guid id);
    public Task<List<Product>> UnapprovedProducts();
    public Task ChangeApprovedProductsStatus(Guid id);
}