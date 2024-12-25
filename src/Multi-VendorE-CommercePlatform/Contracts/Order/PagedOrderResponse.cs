namespace Multi_VendorE_CommercePlatform.Contracts.Order;

public class PagedOrderResponse
{
    public OrderResponse Order { get; set; }
    public List<OrderItemResponse> Items { get; set; }
    public decimal TotalPrice { get; set; }
    public int TotalQuantity { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    
}