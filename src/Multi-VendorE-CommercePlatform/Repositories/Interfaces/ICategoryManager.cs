using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface ICategoryManager
{
    public Task<Category> GetCategory(Guid id);
    public Task<List<Category>> GetAllCategories();
    public Task Update(Category category);
    public Task Delete(Guid id);
    public Task<bool> DoesExist(Guid id);
    public Task Create(Category category);
}