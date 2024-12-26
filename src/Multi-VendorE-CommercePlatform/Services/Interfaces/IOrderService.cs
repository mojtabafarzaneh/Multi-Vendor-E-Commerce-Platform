using Multi_VendorE_CommercePlatform.Contracts.Order;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface IOrderService
{
    public Task<OrderItemResponse> GetOrderItem(Guid orderItemId);
    public Task<PagedOrderResponse> GetAllOrders(int page, int pageSize);
    //vendors
    public Task<List<OrderResponse>> GetOrder();
    public Task UpdateOrderStatus(UpdateOrderStatus request);
    public Task RemoveOrder(Guid orderId);
}