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

    public async Task<(List<Vendor>, int)> UnapprovedVendors(
        int page, int pageSize, string? search)
    {
        var query = _context.Vendors
            .Where(x => x.Approved == false);
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x=> x.BusinessName.Contains(search));
        }
        var totalCount = await query.CountAsync();

        var vendors = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return (vendors, totalCount);
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


    public async Task<(List<Product>, int)> UnapprovedProducts(
        int page, int pageSize, string? search)
    {
        try
        {
            var query = _context.Products
                .Where(x => x.IsApproved == false);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x=> x.Name.Contains(search));
            }
            var totalCount = await query.CountAsync();
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return (products, totalCount);
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