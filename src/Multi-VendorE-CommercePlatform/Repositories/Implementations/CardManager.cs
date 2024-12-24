using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Multi_VendorE_CommercePlatform.Contracts.Cards;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Models.Entities;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;

namespace Multi_VendorE_CommercePlatform.Repositories.Implementations;

public class CardManager:ICardManager
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CardManager> _logger;

    public CardManager(ApplicationDbContext context, ILogger<CardManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> DoesUserExist(Guid userId)
    {
        return await _context.Customers.AnyAsync(x => x.UserId == userId);
    }

    public async Task Create(Card card, CardItem cardItem, Product product)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
            cardItem.CardId = result.Entity.Id;
            
            product.Stock -= cardItem.Quantity;
            await _context.CardItems.AddAsync(cardItem);
            await _context.SaveChangesAsync();
            

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> DoesCustomerHasCard(Guid userId)
    {
        return await _context.Cards.Where(x => x.CustomerId == userId)
            .AnyAsync(x => x.IsPaid == true);
    }

    public async Task Checkout(Guid customerId)
    {
        try
        {
            var card = await _context.Cards
                .Where(x => x.CustomerId == customerId)
                .FirstOrDefaultAsync();
            if (card == null)
            {
                throw new ArgumentException("the card does not exist.");
            }

            card.IsPaid = true;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task UpdateQuantity(UpdateCardItem update)
    {
        try
        {
            var cardItem = await _context.CardItems
                .Where(x => x.Id == update.CardItemId)
                .FirstOrDefaultAsync();
            if (cardItem == null)
            {
                throw new ArgumentException("the cardItem does not exist.");
            }
            var product = await GetProductInfo(cardItem.ProductId);
            if (product == null)
            {
                throw new ArgumentException("the product does not exist.");
            }
            cardItem.Quantity += update.Quantity;
            product.Stock -= update.Quantity;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<Customer> CustomerId(Guid userId)
    {
        var customerId = await _context.Customers
            .FirstOrDefaultAsync(x=> x.UserId == userId);
        if (customerId == null)
        {
            return null!;
        }
        return customerId;
    }

    public async Task<Product> GetProductInfo(Guid productId)
    {
        try
        {
            var product = await _context.Products
                .Where(x => x.Id == productId)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                throw new ArgumentException("The product does not exist.");
            }

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<Card> GetCustomerCard(Guid customerId)
    {
        var card = await _context.Cards
            .FirstOrDefaultAsync(x=> x.CustomerId == customerId);
        if (card == null)
        {
            return null!;
        }
        return card;
    }

    public async Task<(List<CardItem>,int)> GetCustomerCardItems(
        Guid cardId,int page, int pageSize)
    {
        var query = _context.CardItems
            .Where(x => x.CardId == cardId);

        var totalCount = await query.CountAsync();
        var cardItems = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        if (!cardItems.Any())
        {
            return (null!, totalCount);
        }
        return (cardItems, totalCount);
    }

    public async Task<bool> DoesCardItemExist(Guid productId)
    {
        return await _context.CardItems.AnyAsync(x => x.ProductId == productId);
    }
    
    public async Task CreateCardItem(CardItem cardItem, Product product)
    {
        try
        {
            product.Stock -= cardItem.Quantity;
            await _context.CardItems.AddAsync(cardItem);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<CardItem> GetCardItemById(Guid cardItemId)
    {
        try
        {
            var cardItem = await _context.CardItems
                .FirstOrDefaultAsync(x=> x.Id == cardItemId);
            if (cardItem == null)
            {
                return null!;
            }

            return cardItem;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task Delete(Guid customerId)
    {
        try
        {
            var getCard = await GetCustomerCard(customerId);
            _context.Cards.Remove(getCard);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task DeleteCardItem(Guid cardItemId)
    {
        try
        {
            var cardItem = await GetCardItemById(cardItemId);
            if (cardItem == null)
            {
                throw new ArgumentException("The card item does not exist.");
            }

            _context.CardItems.Remove(cardItem);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}