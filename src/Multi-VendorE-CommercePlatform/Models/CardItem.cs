namespace Multi_VendorE_CommercePlatform.Models;

public class CardItem
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public Card Card { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}