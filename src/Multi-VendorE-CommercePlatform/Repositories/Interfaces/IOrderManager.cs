using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface IOrderManager
{
    public Task<bool> DoesUserExist(Guid userGuid);
    public Task<Customer> GetUser(Guid userGuid);
    public Task Create(Order order, List<OrderItem> orderItem);

}