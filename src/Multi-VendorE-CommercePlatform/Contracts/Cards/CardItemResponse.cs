namespace Multi_VendorE_CommercePlatform.Contracts.Cards;

public class CardItemResponse
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}