using Microsoft.EntityFrameworkCore;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Models.Entities;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;

namespace Multi_VendorE_CommercePlatform.Repositories.Implementations;

public class CategoryManager : ICategoryManager
{
    private ApplicationDbContext _context;

    public CategoryManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Category> GetCategory(Guid id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x=>x.Id == id);
        if (category == null)
        {
            return null!;
        }
        return category;
    }

    public async Task<List<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task Update(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var category = await GetCategory(id);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesExist(Guid id)
    {
        return await _context.Categories.AnyAsync(x => x.Id == id);
    }

    public async Task Create(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }
}