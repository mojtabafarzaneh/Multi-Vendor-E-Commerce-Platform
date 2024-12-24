using Multi_VendorE_CommercePlatform.Models;
namespace Multi_VendorE_CommercePlatform.Contracts.Order;

public class OrderResponse
{
    public Guid Id{ get; set; }
    public Guid CustomerId { get; set; }
    public Models.Order.Status OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}