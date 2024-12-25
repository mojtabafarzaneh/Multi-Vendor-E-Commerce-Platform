using Microsoft.EntityFrameworkCore;
using Multi_VendorE_CommercePlatform.Contracts.Order;
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

    public async Task<Order> GetOrder(Guid customerId)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x=> x.CustomerId == customerId);
        if (order == null)
        {
            return null!;
        }
        return order;
    }

    public async Task<OrderItem> GetOrderItem(Guid orderItemId)
    {
        var orderItem = await _context.OrderItems
            .FirstOrDefaultAsync(x=> x.Id == orderItemId);
        if (orderItem == null)
        {
            return null!;
        }
        return orderItem;
    }

    public async Task<List<Order>> GetOrders(Guid customerId)
    {
        var order = await _context.Orders
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();
        if (!order.Any())
        {
            return null!;
        }
        return order;
    }

    public async Task UpdateStatus(UpdateOrderStatus request)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x=> x.Id == request.OrderId);
        if (order != null)
            order.OrderStatus = request.OrderStatus;
        await _context.SaveChangesAsync();
    }
}