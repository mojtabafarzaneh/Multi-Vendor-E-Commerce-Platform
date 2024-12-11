namespace Multi_VendorE_CommercePlatform.Models;

public class Order
{
    public enum Status
    {
        Pending = 1,
        Shipped = 2,
        Delivered = 3,
        Canceled = 4
    }

    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Status OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public Customer Customer { get; set; }
}