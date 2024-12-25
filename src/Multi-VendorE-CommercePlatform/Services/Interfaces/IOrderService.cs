using Multi_VendorE_CommercePlatform.Contracts.Order;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface IOrderService
{
    public Task<OrderItemResponse> GetOrderItem(Guid orderItemId);
    public Task<PagedOrderResponse> GetAllOrders(int page, int pageSize, string search);
    //vendors
    public Task<OrderResponse> GetOrder(Guid orderId);
    public Task UpdateOrderStatus();
    public Task RemoveOrder();
}