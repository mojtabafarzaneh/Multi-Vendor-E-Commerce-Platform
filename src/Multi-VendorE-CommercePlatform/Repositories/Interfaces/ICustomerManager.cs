using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface ICustomerManager
{
    public Task Create(Customer customer);
    public Task<bool> DoesUserExist(Guid userId);
    public Task<Customer> GetUser(Guid userId);
    public Task<Card> GetCard(Guid userId);
    public Task<List<CardItem>> GetCardItems(Guid cardId);
    public Task<List<OrderItem>> GetOrderItems(Guid orderId);
    public Task<Order> GetOrder(Guid customerId);
}