using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface IVendorManager
{
    public Task Create(Vendor vendor);
    public Task<bool> DoesVendorExist(Vendor vendor);

    public Task UpdateProductStock();
    public Task UpdateProductPrice();
    public Task<bool> DoesVendorExist(Guid id);
    public Task<Vendor> GetVendorById(Guid id);
    public Task UpdateEmail(Guid id, string email);
    public Task Delete(Vendor vendor);
}