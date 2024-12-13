using Microsoft.EntityFrameworkCore;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Models.Entities;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;

namespace Multi_VendorE_CommercePlatform.Repositories.Implementations;

public class ProductManager : IProductManager
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductManager> _logger;

    public ProductManager(ApplicationDbContext context,
        ILogger<ProductManager> logger)
    {
        _context = context;
        _logger = logger;
    }
    //TODO: Implement The product stock and price in vendors Manager
    public async Task Create(Product product)
    {
        try
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> DoesExist(Guid id)
    {
        try
        {
            return await _context.Products.AnyAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<(List<Product>, int)> GetAll(Guid vendorId, int page, int pageSize, string? searchString)
    {
        try
        {
            var query = _context.Products
                .Where(x => x.VendorId == vendorId);
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString)); 
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

    public async Task<Product> GetById(Guid id)
    {
        try
        {
            var vendor = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
            if (vendor == null)
            {
                return null!;
            }

            return vendor;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task UpdateNameAndDescription(UpdateProductNameAndDescription update)
    {
        try
        {
            var product = _context.Products
                .FirstOrDefault(x => x.Id == update.Id);
            if (product == null)
            {
                throw new ArgumentException("Product not found");
            }
            product.Name = update.Name;
            product.Description = update.Description;
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> IsApproved(Guid id)
    { 
        try 
        {
            return await _context.Products
                .Where(x=> x.Id == id)
                .AnyAsync(x=> x.IsApproved == true);
        }
        catch (Exception e) 
        { 
            Console.WriteLine(e); 
            throw;
        }
    }

    public async Task<bool> DoesVendorExist(Guid id)
    {
        try
        {
            return await _context.Vendors
                .AnyAsync(x=> x.UserId == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<Guid> VendorId(Guid id)
    {
        var vendor = await _context.Vendors
            .FirstOrDefaultAsync(x => x.UserId == id);
        if (vendor == null)
        {
            return Guid.Empty;
        }

        return vendor.Id;
    }

    public async Task<bool> DoesCategoryExist(Guid id)
    {
        try
        {
            return await _context.Categories
                .AnyAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> DoesExist(string name)
    {
        try
        {
            return await _context.Products.AnyAsync(x => x.Name == name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}