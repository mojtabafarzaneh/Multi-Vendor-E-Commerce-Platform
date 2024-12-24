using Microsoft.AspNetCore.Mvc.RazorPages;
using Multi_VendorE_CommercePlatform.Contracts.Cards;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface ICardService
{
    public Task CreateCard(CreateCardItemRequest card);
    //admin public Task GetAllCards();
    public Task<PagedCardResponse> CustomerCards(int page, int pageSize);
    public Task<CardItemResponse> GetCardItemById(Guid cardItemId);
    public Task CreateCardItem(CreateCardItemRequest card);
    public Task RemoveCard();
    public Task RemoveCardItemById(Guid cardItemId);
    public Task UpdateCardItem(UpdateCardItem cardItem);
    
}