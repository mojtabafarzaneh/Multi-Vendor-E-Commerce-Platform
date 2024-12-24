namespace Multi_VendorE_CommercePlatform.Contracts.Cards;

public class PagedCardResponse
{
    public CardResponse Card { get; set; }
    public List<CardItemResponse> Items { get; set; }
    public decimal TotalPrice { get; set; }

    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}