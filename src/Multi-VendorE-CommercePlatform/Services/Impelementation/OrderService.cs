using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Cards;
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
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!await _orderManager.DoesUserExist(userGuid))
            {
                throw new UnauthorizedAccessException("you can not reach this endpoint");
            }
            var customer = await _orderManager.GetUser(userGuid);
            var order = await _orderManager.GetOrder(customer.Id);
            if (order == null)
            {
                throw new ArgumentException("There are no orders for this user.");
            }
            var orderItem = await _orderManager
                .GetOrderItem(orderItemId);
            if (orderItem == null)
            {
                throw new ArgumentException("Invalid orderItemId.");
            }

            if (order.Id != orderItem.OrderId)
            {
                throw new ArgumentException("The orderId does not match the orderItemId.");
            }

            var mappedResponse = _mapper.Map<OrderItemResponse>(orderItem);
            mappedResponse.TotalPrice = orderItem.Price * orderItem.Quantity;
            return mappedResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<PagedOrderResponse> GetAllOrders(
        int page, int pageSize)
    {
        try
        {
            var userId = _userHelper.UserId();
            var isVendor = _roleHelper.IsVendorUser();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!isVendor)
            {
                throw new UnauthorizedAccessException("you can not reach this endpoint");
            }

            var orders = await _orderManager.GetAllOrders();
            var pagedOrderResponses = new List<SingleOrderResponse>();
            decimal totalPrice = 0;
            int totalQuantity = 0;

            foreach (var order in orders)
            {
                var (oi, totalCount) = await _orderManager
                    .GetOrderItem(order.Id, page, pageSize);
                if (!oi.Any()) continue;
                
                var mappedOrder = _mapper.Map <SingleOrderResponse>(order);
                var mappedOrderItems = _mapper.Map <List<OrderItemResponse>>(oi);
                foreach (var oiTotalPrice in mappedOrderItems)
                {
                    oiTotalPrice.TotalPrice = oiTotalPrice.Price * oiTotalPrice.Quantity;
                    totalPrice += oiTotalPrice.TotalPrice;
                    totalQuantity += oiTotalPrice.Quantity;
                }

                mappedOrder.Items = mappedOrderItems;

                pagedOrderResponses.Add(mappedOrder);
            }

            return new PagedOrderResponse
            {
                Order = pagedOrderResponses,
                TotalPrice = totalPrice,
                TotalQuantity = totalQuantity,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((decimal)totalQuantity / pageSize)
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<List<OrderResponse>> GetOrder()
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!await _orderManager.DoesUserExist(userGuid))
            {
                throw new UnauthorizedAccessException("you can not reach this endpoint");
            }

            var customer = await _orderManager.GetUser(userGuid);
            var order = await _orderManager.GetOrders(customer.Id);
            if (order == null)
            {
                throw new ArgumentException("There are no orders for this user.");
            }
            var mappedOrder = _mapper.Map<List<OrderResponse>>(order);
            return mappedOrder;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task UpdateOrderStatus(UpdateOrderStatus request)
    {
        try
        {
            var userId = _userHelper.UserId();
            var isVendor = _roleHelper.IsVendorUser();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!isVendor)
            {
                throw new UnauthorizedAccessException("you can not reach this endpoint");
            }

            await _orderManager.UpdateStatus(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }

    }

    public async Task RemoveOrder(Guid orderId)
    {
        try
        {
            var userId = _userHelper.UserId();
            var isVendor = _roleHelper.IsVendorUser();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");
            if (!isVendor)
            {
                throw new UnauthorizedAccessException("you can not reach this endpoint");
            }

            await _orderManager.Remove(orderId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}