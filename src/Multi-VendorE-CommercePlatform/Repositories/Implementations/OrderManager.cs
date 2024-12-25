using Microsoft.EntityFrameworkCore;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Models.Entities;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;

namespace Multi_VendorE_CommercePlatform.Repositories.Implementations;

public class OrderManager: IOrderManager
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OrderManager> _logger;

    public OrderManager(ApplicationDbContext context,
        ILogger<OrderManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> DoesUserExist(Guid userGuid)
    {
        return await _context.Customers.AnyAsync(x=> x.UserId == userGuid);
    }

    public async Task<Customer> GetUser(Guid userGuid)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x=> x.UserId == userGuid);
        if (customer == null)
        {
            return null!;
        }
        return customer;
    }
    

    public async Task Create(Order order, List<OrderItem> orderItem)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach (var or in orderItem)
            {
                or.OrderId = result.Entity.Id;
                await _context.OrderItems.AddAsync(or);
                await _context.SaveChangesAsync();
            }
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