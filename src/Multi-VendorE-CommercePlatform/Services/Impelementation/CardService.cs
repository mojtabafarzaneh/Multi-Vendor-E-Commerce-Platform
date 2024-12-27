using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Cards;
using Multi_VendorE_CommercePlatform.Helpers;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;
using RabbitMqBrokerLibrary;
using RabbitMqBrokerLibrary.Broker;

namespace Multi_VendorE_CommercePlatform.Services.Implenetations;

public class CardService: ICardService
{
    private readonly ILogger<CardService> _logger;
    private readonly ICardManager _cardManager;
    private readonly UserHelper _userHelper;
    private readonly RoleHelper _roleHelper;
    private readonly IMapper _mapper;

    public CardService(ILogger<CardService> logger,
        ICardManager cardManager,
        UserHelper userHelper,
        RoleHelper roleHelper,
        IMapper mapper)
    {
        _logger = logger;
        _cardManager = cardManager;
        _userHelper = userHelper;
        _roleHelper = roleHelper;
        _mapper = mapper;
    }

    public async Task CreateCard(CreateCardItemRequest card)
    {
        try
        {
            if (card.Quantity < 1 || card.Quantity > 100)
            {
                throw new ArgumentNullException(
                    $"the {nameof(card)} parameter cannot be null");
            }
            var userId = _userHelper.UserId();
            var isAdmin = _roleHelper.IsAdminUser();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!isAdmin && !await _cardManager.DoesUserExist(userGuid))
            {
                throw new 
                    UnauthorizedAccessException("you can not reach this endpoint");
            }
            var customer = await _cardManager.CustomerId(userGuid);
            if (customer == null)
            {
                throw new ArgumentException("Invalid Customer Id.");
            }

            if (await _cardManager.DoesCustomerHasCard(customer.Id))
            {
                throw new ArgumentException("You have already created your card.");
            }
            var creatingCard = new Card
            {
                CustomerId = customer.Id,
            };

            var product = await _cardManager
                .GetProductInfo(card.ProductId);
            if (!product.IsApproved)
            {
                throw new ArgumentException("this product has not been approved.");
            }
            if (product.Stock < card.Quantity)
            {
                throw new ArgumentException("the are not enough product in stock");
            }
            var cardItem = _mapper.Map<CardItem>(card);
            cardItem.Price = product.Price;
            await _cardManager.Create(creatingCard, cardItem, product);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<PagedCardResponse> CustomerCards(int page, int pageSize)
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!await _cardManager.DoesUserExist(userGuid))
            {
                throw new 
                    UnauthorizedAccessException("you can not reach this endpoint");
            }
            var customer = await _cardManager.CustomerId(userGuid);
            if (customer == null)
            {
                throw new ArgumentException("Invalid Customer Id.");
            }
            var card = await _cardManager.GetCustomerCard(customer.Id);
            if (card == null)
            {
                throw new ArgumentException("Invalid Customer Id.");
            }

            var (cardItem, totalCount) = await _cardManager
                .GetCustomerCardItems(card.Id, page, pageSize);


            var cardMapper = _mapper.Map<CardResponse>(card);
            var cardItemsMapper = _mapper.Map<List<CardItemResponse>>(cardItem);
            
            decimal totalPrice = 0;
            foreach (var ci in cardItemsMapper)
            {
                ci.TotalPrice =
                    ci.Quantity * ci.Price;
                totalPrice += ci.TotalPrice;
            }
            return new PagedCardResponse
            {
                Card = cardMapper,
                Items = cardItemsMapper,
                TotalPrice = totalPrice,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
    }

    public async Task<CardItemResponse> GetCardItemById(Guid cardItemId)
    {
        try
        {

            var userId = _userHelper.UserId();
            var isAdmin = _roleHelper.IsAdminUser();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!await _cardManager.DoesUserExist(userGuid) && !isAdmin)
            { 
                throw new 
                    UnauthorizedAccessException("you can not reach this endpoint");
            }
            var cardItem = await _cardManager.GetCardItemById(cardItemId);
            var response = _mapper.Map<CardItemResponse>(cardItem);
            response.TotalPrice = cardItem.Quantity * cardItem.Price;
            return response;


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task CreateCardItem(CreateCardItemRequest request)
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!await _cardManager.DoesUserExist(userGuid))
            {
                throw new 
                    UnauthorizedAccessException("you can not reach this endpoint");
            }
            var customer = await _cardManager.CustomerId(userGuid);
            if (customer == null)
            {
                throw new ArgumentException("Invalid Customer Id.");
            }
            var productInfo = await _cardManager.GetProductInfo(request.ProductId);
            if (productInfo == null)
            {
                throw new ArgumentException("Invalid Product Id.");
            }
            if (!productInfo.IsApproved)
            {
                throw new ArgumentException("this product has not been approved.");
            }
            if (productInfo.Stock < request.Quantity)
            {
                throw new ArgumentException("the are not enough product in stock");
            }

            if (await _cardManager.DoesCardItemExist(request.ProductId))
            {
                throw new ArgumentException(
                    "this product has already been added to your card," +
                    " you can add the quantity in the update endpoint.");
            }
            
            var card = await _cardManager.GetCustomerCard(customer.Id);
            var mappedCardItem = _mapper.Map<CardItem>(request);
            mappedCardItem.CardId = card.Id;
            mappedCardItem.Price = productInfo.Price;
            
            await _cardManager.CreateCardItem(mappedCardItem, productInfo);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
    }

    public async Task RemoveCard()
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!await _cardManager.DoesUserExist(userGuid))
            {
                throw new 
                    UnauthorizedAccessException("you can not reach this endpoint");
            }
            var customer = await _cardManager.CustomerId(userGuid);
            if (customer == null)
            {
                throw new ArgumentException("Invalid Customer Id.");
            }

            await _cardManager.Delete(customer.Id);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task RemoveCardItemById(Guid cardItemId)
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!await _cardManager.DoesUserExist(userGuid))
            {
                throw new 
                    UnauthorizedAccessException("you can not reach this endpoint");
            }

            await _cardManager.DeleteCardItem(cardItemId);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task CheckOutCard()
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!await _cardManager.DoesUserExist(userGuid))
            {
                throw new 
                    UnauthorizedAccessException("you can not reach this endpoint");
            }
            var customer = await _cardManager.CustomerId(userGuid);
            if (customer == null)
            {
                throw new ArgumentException("Invalid Customer Id.");
            }
            var card = await _cardManager.GetCard(customer.Id);
            var cardItems = await _cardManager.GetCardItem(card.Id);
            var order = new Order
            {
                OrderStatus = Order.Status.Pending,
                CustomerId = customer.Id,
            };
            var orderItems = new List<OrderItem>();
            foreach (var cardItem in cardItems)
            {
                orderItems.Add(new OrderItem
                {
                    ProductId = cardItem.ProductId,
                    Quantity = cardItem.Quantity,
                    Price = cardItem.Price
                });
            }

            var emailProducer = new RabbitMqProducer();
            
            emailProducer.PublishEmailEvent(new EmailMessage
            {
                To = customer.User.Email!,
                Subject = "Order Confirmation",
                Body = "Your order has been Placed successfully.",
            });

            await _cardManager.Checkout(customer.Id, order, orderItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task UpdateCardItem(UpdateCardItem cardItem)
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!await _cardManager.DoesUserExist(userGuid))
            {
                throw new 
                    UnauthorizedAccessException("you can not reach this endpoint");
            }
            var customer = await _cardManager.CustomerId(userGuid);
            if (customer == null)
            {
                throw new ArgumentException("Invalid Customer Id.");
            }

            await _cardManager.UpdateQuantity(cardItem);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}