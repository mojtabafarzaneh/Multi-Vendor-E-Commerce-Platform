using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface IProductService
{
    public Task AddProduct(ProductRequest request);
    public Task<PagedProductResponse> GetAllProducts(
        int page, int pageSize, string? search, bool isApproved);
    public Task<ProductResponse> GetProductById(Guid id);
    public Task UpdateNameAndDescription(UpdateProductNameAndDescription request, Guid id);
    public Task RemoveProduct(Guid id);
}