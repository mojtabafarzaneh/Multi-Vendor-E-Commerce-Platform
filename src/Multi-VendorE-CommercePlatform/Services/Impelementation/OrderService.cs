using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Order;
using Multi_VendorE_CommercePlatform.Helpers;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Services.Implenetations;

public class OrderService: IOrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;
    private readonly RoleHelper _roleHelper;
    private readonly UserHelper _userHelper;
    private readonly IOrderManager _orderManager;

    public OrderService(ILogger<OrderService> logger, IMapper mapper, RoleHelper roleHelper, UserHelper userHelper, IOrderManager orderManager)
    {
        _logger = logger;
        _mapper = mapper;
        _roleHelper = roleHelper;
        _userHelper = userHelper;
        _orderManager = orderManager;
    }

    

    public async Task<OrderItemResponse> GetOrderItem(Guid orderItemId)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedOrderResponse> GetAllOrders(int page, int pageSize, string search)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderResponse> GetOrder(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateOrderStatus()
    {
        throw new NotImplementedException();
    }

    public async Task RemoveOrder()
    {
        throw new NotImplementedException();
    }
}