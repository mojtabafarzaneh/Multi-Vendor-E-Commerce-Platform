using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_VendorE_CommercePlatform.Contracts.Cards;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Controllers;

[ApiController]
public class CardController:ControllerBase
{
    private readonly ICardService _cardService;

    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpPost(ApiEndpoints.Card.Create)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> CreateCard([FromBody] CreateCardItemRequest request)
    {
        if (request.Quantity == 0 || request.Quantity < 0 || request.Quantity > 100)
        {
            return BadRequest("Quantity must be between 0 and 100");
        }

        if (request.ProductId == Guid.Empty)
        {
            return BadRequest("ProductId must be provided");
        }
        await _cardService.CreateCard(request);
        return Ok();
    }

    [HttpGet(ApiEndpoints.Card.CustomerCard)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> GetCustomerCard(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1 || pageSize < 1 || pageSize > 25)
        {
            return BadRequest("Page must be bigger than 1 and pageSize should be between 1 and 25");
        }
        var response = await _cardService.CustomerCards(page, pageSize);
        return Ok(response);
        
    }
    [HttpDelete(ApiEndpoints.Card.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> DeleteCard()
    {
        await _cardService.RemoveCard();
        return Ok();
    }

    [HttpPost(ApiEndpoints.Card.CreateCardItem)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> CreateCardItem([FromBody] CreateCardItemRequest request)
    {
        if (request.Quantity == 0 || request.Quantity < 0 || request.Quantity > 100)
        {
            return BadRequest("Quantity must be between 0 and 100");
        }

        if (request.ProductId == Guid.Empty)
        {
            return BadRequest("ProductId must be provided");
        }
        await _cardService.CreateCardItem(request);
        return Ok();
    }

    [HttpGet(ApiEndpoints.Card.GetCardItemById)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> ResponseCardItem([FromRoute] Guid id)
    {
        var response = await _cardService.GetCardItemById(id);
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Card.DeleteCardItem)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<IActionResult> DeleteCardItem([FromRoute] Guid id)
    {
        var stringCardItem = id.ToString();
        if (stringCardItem == null!)
        {
            return BadRequest("CardItemId must be provided");
        }

        await _cardService.RemoveCardItemById(id);
        return Ok();
    }
}