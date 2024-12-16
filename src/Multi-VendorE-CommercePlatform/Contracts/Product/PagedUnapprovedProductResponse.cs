namespace Multi_VendorE_CommercePlatform.Contracts.Project;

public class PagedUnapprovedProductResponse
{
    public List<UnApproveProductResponse> Items { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}