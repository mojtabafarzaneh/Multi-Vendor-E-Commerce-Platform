using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface IProductManager
{
    public Task Create(Product product);
    public Task<bool> DoesExist(Guid id);
    public Task<(List<Product>, int)> GetAll(
        Guid vendorId, int page, int pageSize, string? search, bool isApproved);
    public Task<Product> GetById(Guid id);
    public Task UpdateNameAndDescription(UpdateProductNameAndDescription update);
    public Task<bool> IsApproved(Guid id);
    public Task<bool> DoesVendorExist(Guid id);
    public Task<Guid> VendorId(Guid id);
    public Task<bool> DoesCategoryExist(Guid id);
    public Task<bool> DoesExist(string name);
    public Task Delete(Guid id);
}