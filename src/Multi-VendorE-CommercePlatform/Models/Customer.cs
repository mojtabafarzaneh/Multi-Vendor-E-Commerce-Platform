namespace Multi_VendorE_CommercePlatform.Models;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    
    public ICollection<Order> Orders { get; set; }
    public ICollection<Card> Cards { get; set; }
}