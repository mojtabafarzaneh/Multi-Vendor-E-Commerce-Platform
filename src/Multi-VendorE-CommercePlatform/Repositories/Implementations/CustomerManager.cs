using Microsoft.EntityFrameworkCore;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Models.Entities;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;

namespace Multi_VendorE_CommercePlatform.Repositories.Implementations;

public class CustomerManager : ICustomerManager
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

    public async Task<bool> DoesUserExist(Guid userId)
    {
        return await _context.Customers.AnyAsync(x => x.UserId == userId);
    }

    public async Task<Customer> GetUser(Guid userId)
    {
        try
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (customer == null)
            {
                return null!;
            }

            return customer;
        }
        catch (Exception)
        {
            _logger.LogError($"Failed to get customer from database: {userId}");
            throw;
        }
    }

    public async Task<Card> GetCard(Guid customerId)
    {
        var card = await _context.Cards
            .FirstOrDefaultAsync(x => x.CustomerId == customerId);
        if (card == null)
        {
            return null!;
        }

        return card;
    }

    public async Task<List<CardItem>> GetCardItems(Guid cardId)
    {
        if (cardId == Guid.Empty)
        {
            return null!;
        }
        var cardItem = await _context.CardItems
            .Where(c => c.CardId == cardId)
            .ToListAsync();
        if (!cardItem.Any())
        {
            return null!;
        }

        return cardItem;
    }

    public async Task<List<OrderItem>> GetOrderItems(Guid orderId)
    {
        if (orderId == Guid.Empty)
        {
            return null!;
        }
        var orderItem = await _context.OrderItems
            .Where(o => o.OrderId == orderId)
            .ToListAsync();

        return orderItem!;
    }

    public async Task<Order> GetOrder(Guid customerId)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.CustomerId == customerId);

        return order!;
    }
    

}