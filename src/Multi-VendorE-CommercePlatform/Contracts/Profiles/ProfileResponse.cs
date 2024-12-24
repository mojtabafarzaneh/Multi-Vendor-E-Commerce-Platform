using Multi_VendorE_CommercePlatform.Contracts.Cards;
using Multi_VendorE_CommercePlatform.Contracts.Order;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Contracts.Profiles;

public class ProfileResponse
{
    public CustomerResponse Customer { get; set; }
    public OrderResponse OrderResponse { get; set; }
    public List<OrderItemResponse> OrderItemResponse { get; set; }
    public CardResponse CardResponses { get; set; }
    public List<CardItemResponse> CardItemResponses { get; set; }
    
}