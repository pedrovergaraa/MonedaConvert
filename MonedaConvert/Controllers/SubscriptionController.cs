using CurrencyConvert.Models.Dtos;
using CurrencyConvert.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/subscription")]
[ApiController]
[Authorize]
public class SubscriptionController : ControllerBase

{
    private readonly SubscriptionService _subscriptionService;

    public SubscriptionController(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    // Obtener todas las suscripciones
    [HttpGet("all")]
    public IActionResult GetAllSubscriptions()
    {
        try
        {
            var subscriptions = _subscriptionService.GetAllSubscriptions();
            return Ok(subscriptions);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [HttpGet("userSub/{userId}")]
    public IActionResult GetUserSubscription(int userId)
    {
        var subscription = _subscriptionService.GetUserSubscription(userId);
        return Ok(subscription);
    }


    [HttpPut("update")]
    public IActionResult UpdateSubscription([FromBody] ActivateSubscriptionDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found.");
            }

            int userId = int.Parse(userIdClaim.Value);
            _subscriptionService.UpdateUserSubscription(userId, dto);

            return Ok(new { Message = "Subscription updated successfully!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }


}
