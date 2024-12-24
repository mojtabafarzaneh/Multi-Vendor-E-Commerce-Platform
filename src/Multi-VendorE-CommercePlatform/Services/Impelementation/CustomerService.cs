using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Cards;
using Multi_VendorE_CommercePlatform.Contracts.Order;
using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Helpers;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Repositories.Implementations;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Services.Implenetations;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerService> _logger;
    private readonly UserHelper _userHelper;
    private readonly RoleHelper _roleHelper;
    private readonly ICustomerManager _customerManager;

    public CustomerService(IMapper mapper, 
        ILogger<CustomerService> logger, 
        UserHelper userHelper, 
        RoleHelper roleHelper, 
        ICustomerManager customerManager)
    {
        _mapper = mapper;
        _logger = logger;
        _userHelper = userHelper;
        _roleHelper = roleHelper;
        _customerManager = customerManager;
    }

    public async Task<ProfileResponse> Profile()
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!await _customerManager.DoesUserExist(userGuid))
            {
                throw new UnauthorizedAccessException("you can not reach this endpoint");
            }

            var customer = await _customerManager.GetUser(userGuid) ?? 
                           new Customer();
            var card = await _customerManager.GetCard(customer.Id)?? 
                       new Card();
            var cardItem = await _customerManager.GetCardItems(card.Id) ?? 
                           new List<CardItem>();
            var order = await _customerManager.GetOrder(customer.Id) ?? 
                        new Order();
            var orderItem = await _customerManager.GetOrderItems(order.Id)?? new 
                List<OrderItem>(); 
            
            var customerResponse = _mapper.Map<CustomerResponse>(customer) ??
                                   new CustomerResponse();
            var cardResponse = _mapper.Map<CardResponse>(card) ??
                               new CardResponse();
            var cardItemResponse = _mapper.
                                       Map<List<CardItemResponse>>(cardItem) ?? 
                                   new List<CardItemResponse>();
            var orderResponse = _mapper.Map<OrderResponse>(order) ?? 
                                new OrderResponse();
            var orderItemResponse = _mapper.
                                        Map<List<OrderItemResponse>>(orderItem) ?? 
                                    new List<OrderItemResponse>();

            return new ProfileResponse
            {
                Customer = customerResponse,
                CardResponses = cardResponse,
                CardItemResponses = cardItemResponse,
                OrderResponse = orderResponse,
                OrderItemResponse = orderItemResponse,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}