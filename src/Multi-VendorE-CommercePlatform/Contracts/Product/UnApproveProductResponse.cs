namespace Multi_VendorE_CommercePlatform.Contracts.Project;

public class UnApproveProductResponse
{
    public Guid Id { get; set; }
    public Guid VendorId { get; set; }
    public string Name { get; set; }
    public bool IsApproved { get; set; }
}