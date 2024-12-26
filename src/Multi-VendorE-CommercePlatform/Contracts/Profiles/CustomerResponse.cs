using Multi_VendorE_CommercePlatform.Contracts.Cards;
using Multi_VendorE_CommercePlatform.Contracts.Order;

namespace Multi_VendorE_CommercePlatform.Contracts.Profiles;

public class CustomerResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
    public List<CardResponse> Card { get; set; }
    public List<OrderResponse> Order { get; set; }
}