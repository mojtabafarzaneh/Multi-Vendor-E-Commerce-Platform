using Multi_VendorE_CommercePlatform.Contracts.Order;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface IOrderManager
{
    public Task<bool> DoesUserExist(Guid userGuid);
    public Task<Customer> GetUser(Guid userGuid);
    public Task<Order> GetOrder(Guid customerId);
    public Task<List<Order>> GetOrders(Guid customerId);
    public Task UpdateStatus(UpdateOrderStatus request);
    public Task<OrderItem> GetOrderItem(Guid customerId);

}