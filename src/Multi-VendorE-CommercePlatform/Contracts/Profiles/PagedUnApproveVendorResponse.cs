namespace Multi_VendorE_CommercePlatform.Contracts.Profiles;

public class PagedUnApproveVendorResponse
{
    public List<UnApproveVendorResponse> Items { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    
}