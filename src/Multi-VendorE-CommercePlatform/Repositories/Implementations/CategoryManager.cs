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
}