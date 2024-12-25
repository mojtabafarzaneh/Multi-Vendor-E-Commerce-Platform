using Microsoft.EntityFrameworkCore.Infrastructure;
using Multi_VendorE_CommercePlatform.Contracts.Cards;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface ICardManager
{
    public Task<bool> DoesUserExist(Guid userId);
    public Task Create(Card card, CardItem cardItem, Product product);
    public Task<bool> DoesCustomerHasCard(Guid userId);
    public Task<Customer> CustomerId(Guid userId);
    public Task<Product> GetProductInfo(Guid productId);
    public Task<Card> GetCustomerCard(Guid customerId);
    public Task<(List<CardItem>, int)> GetCustomerCardItems(
        Guid cardId, int page, int pageSize);
    public Task<bool> DoesCardItemExist(Guid productId);
    public Task Delete(Guid customerId);
    public Task CreateCardItem(CardItem cardItem, Product product);
    public Task Checkout(Guid customerId, Order order, List<OrderItem> orderItem);
    public Task UpdateQuantity(UpdateCardItem update);
    public Task<CardItem> GetCardItemById(Guid cardItemId);
    public Task<bool> DoesProductExist(Guid productId);
    public Task DeleteCardItem(Guid cardItemId);
    public Task<Card> GetCard(Guid customerId);
    public Task<List<CardItem>> GetCardItem(Guid cardId);

}