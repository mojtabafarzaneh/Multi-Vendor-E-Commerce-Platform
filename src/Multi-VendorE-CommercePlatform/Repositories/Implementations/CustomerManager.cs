using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Models.Entities;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;

namespace Multi_VendorE_CommercePlatform.Repositories.Implementations;

public class CustomerManager: ICustomerManager
{
    
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CustomerManager> _logger;

    public CustomerManager(ApplicationDbContext context, ILogger<CustomerManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Create(Customer customer)
    {
        try
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError($"Failed to add customer to database: {customer.Id}");
            throw;
        }
        
    }
}