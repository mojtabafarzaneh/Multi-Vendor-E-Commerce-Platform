namespace Multi_VendorE_CommercePlatform.Contracts.Order;

public class UpdateOrderStatus
{
    public Guid OrderId { get; set; }
    public Models.Order.Status OrderStatus { get; set; }
}