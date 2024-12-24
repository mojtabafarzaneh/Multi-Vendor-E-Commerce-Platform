namespace Multi_VendorE_CommercePlatform.Contracts.Cards;

public class CreateCardItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}