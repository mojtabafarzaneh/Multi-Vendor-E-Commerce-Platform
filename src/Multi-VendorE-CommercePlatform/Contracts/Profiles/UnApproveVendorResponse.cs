namespace Multi_VendorE_CommercePlatform.Contracts.Profiles;

public class UnApproveVendorResponse
{
    public Guid Id { get; set; }
    public string BusinessName { get; set; }
    public bool Approved { get; set; }
}