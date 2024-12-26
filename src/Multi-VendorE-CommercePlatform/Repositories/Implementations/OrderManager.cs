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

    public async Task Remove(Guid orderId)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x=> x.Id == orderId);
        if (order == null)
        {
            throw new ArgumentException("Order not found");
        }
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Vendor> GetVendor(Guid userId)
    {
        var vendor = await  _context.Vendors
            .FirstOrDefaultAsync(x=> x.UserId == userId);
        if (vendor == null)
        {
            return null!;
        }
        return vendor;
    }

    public async Task<Product> GetProduct(Guid vendorId)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(x=> x.VendorId == vendorId);
        if (product == null)
        {
            return null!;
        }
        return product;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        var orders = await _context.Orders
            .ToListAsync();
        if (!orders.Any())
        {
            return null!;
        }
        return orders;
    }

    public async Task<(List<OrderItem>, int)> GetOrderItem(
        Guid orderId, int page, int pageSize)
    {
        var query = _context.OrderItems
            .Where(oi => oi.OrderId == orderId);
        
        var totalCount = await query.CountAsync();
        var orderItems = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        if (!query.Any())
        {
            return (null!, 0);
        }
        return (orderItems, totalCount);
    }
}