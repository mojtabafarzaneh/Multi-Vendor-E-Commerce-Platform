using Multi_VendorE_CommercePlatform.Contracts.Order;

namespace Multi_VendorE_CommercePlatform.Contracts.Cards;

public class SingleOrderResponse
{
    public Guid Id{ get; set; }
    public Guid CustomerId { get; set; }
    public Models.Order.Status OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<OrderItemResponse> Items { get; set; }
}