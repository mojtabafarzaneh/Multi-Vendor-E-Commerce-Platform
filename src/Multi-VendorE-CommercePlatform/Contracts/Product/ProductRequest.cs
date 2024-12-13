namespace Multi_VendorE_CommercePlatform.Contracts.Project;

public class ProductRequest
{
    public Guid CategoryId { get; set; }
    public int Stock { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
}