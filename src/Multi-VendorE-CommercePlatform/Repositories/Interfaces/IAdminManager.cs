using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface IAdminManager
{
    public Task<bool> DoesAdminExist(Guid id);
    public Task<(List<Vendor>, int)> UnapprovedVendors(
        int page, int pageSize, string? search);
    public Task ChangeApprovedVendorsStatus(Guid id);
    public Task<(List<Product>, int)> UnapprovedProducts(
        int page, int pageSize, string? search);
    public Task ChangeApprovedProductsStatus(Guid id);
    public Task<(List<Product>, int)> GetProducts(
        int page, int pageSize, string? search);

    public Task<List<Vendor>> GetVendors(
        int page, int pageSize);

    public Task<List<Customer>> GetCustomers(
        int page, int pageSize);
    public Task<List<Order>> GetOrders(
        int page, int pageSize);
}