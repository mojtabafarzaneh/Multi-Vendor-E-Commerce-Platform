using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface ICategoryService
{
    public Task<CategoryResponse> GetCategoryByIdAsync(Guid id);
    public Task<List<CategoryResponse>> GetCategoriesAsync();
    public Task Create(CategoryRequest category);
    public Task Update(UpdateCategoryRequest request, Guid id);
    public Task Delete(Guid id);
}