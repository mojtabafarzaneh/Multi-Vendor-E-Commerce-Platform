using Microsoft.EntityFrameworkCore;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Models.Entities;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;

namespace Multi_VendorE_CommercePlatform.Repositories.Implementations;

public class AdminManager : IAdminManager
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AdminManager> _logger;
    
    public AdminManager(ILogger<AdminManager> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    //TODO: ADD THE QUERYABLE FUNCTIONALITY.

    public async Task<bool> DoesAdminExist(Guid id)
    {
        return await _context.Users.AnyAsync(x => x.Id == id);
    }

    public async Task<List<Vendor>> UnapprovedVendors()
    {
        var vendors = await _context.Vendors
            .Where(x => x.Approved == false)
            .ToListAsync();
        return vendors;
    }

    public async Task ChangeApprovedVendorsStatus(Guid id)
    {
        try
        {
            var vendor = await _context.Vendors.FirstOrDefaultAsync(x => x.Id == id);
            if (vendor == null) throw new ArgumentException("Invalid vendor!");

            vendor.Approved = true;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }


    public async Task<List<Product>> UnapprovedProducts()
    {
        try
        {
            var products = await _context.Products
                .Where(x => x.IsApproved == false)
                .ToListAsync();
            return products;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task ChangeApprovedProductsStatus(Guid id)
    {
        try
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product != null) product.IsApproved = true;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}