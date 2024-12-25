using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_VendorE_CommercePlatform.Contracts.Order;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Controllers;

[ApiController]
public class OrderController:ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet(ApiEndpoints.Order.OrderItemPage)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> GetOrderItemById([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest("Invalid Id");
        }

        var res = await _orderService.GetOrderItem(id);
        return Ok(res);
    }

    [HttpGet(ApiEndpoints.Order.GetOrder)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> GetOrder()
    {
        var res = await _orderService.GetOrder();
        return Ok(res);
    }

    [HttpGet(ApiEndpoints.Order.UpdateStatus)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<ActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatus request)
    {
        await _orderService.UpdateOrderStatus(request);
        return Ok();
    }
}