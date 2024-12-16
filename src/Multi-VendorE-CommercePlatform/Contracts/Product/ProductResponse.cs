namespace Multi_VendorE_CommercePlatform.Contracts.Project;

public class ProductResponse
{
    public Guid Id { get; set; }
    public Guid VendorId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsApproved { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
}