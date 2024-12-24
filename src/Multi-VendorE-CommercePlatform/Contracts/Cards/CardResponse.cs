namespace Multi_VendorE_CommercePlatform.Contracts.Cards;

public class CardResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}