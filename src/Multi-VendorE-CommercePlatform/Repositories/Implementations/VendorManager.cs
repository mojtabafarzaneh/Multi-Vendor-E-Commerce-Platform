using Microsoft.EntityFrameworkCore;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Models.Entities;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Implenetations;

namespace Multi_VendorE_CommercePlatform.Repositories.Implementations;

public class VendorManager: IVendorManager
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<VendorService> _logger;

    public VendorManager(ApplicationDbContext context,
        ILogger<VendorService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Create(Vendor vendor)
    {
        try
        {
            await _context.Vendors.AddAsync(vendor);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }

    }

    public async Task<bool> DoesVendorExist(Vendor vendor)
    {
        return await _context.Vendors.AnyAsync(x => x.BusinessEmail == vendor.BusinessEmail);
    }

    public async Task<bool> DoesVendorExist(Guid id)
    {
        return await _context.Vendors.AnyAsync(x => x.UserId == id);
    }

    public async Task<Vendor> GetVendorById(Guid id)
    { 
        var user =  await _context.Vendors
            .Include(v => v.Products)
            .FirstOrDefaultAsync(x => x.UserId == id);
        if (user == null)
        {
            return null;
        }
        return user;
    }

    public async Task UpdateEmail(Guid id, string email)
    {
        var vendor = await _context.Vendors
            .FirstOrDefaultAsync(x => x.UserId == id);
        if (vendor == null)
        {
            return;
        }
        vendor.BusinessEmail = email;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Vendor vendor)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var user = await _context.Users
                .Where(x => x.Id == vendor.UserId).FirstOrDefaultAsync();
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            if (user != null) _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}