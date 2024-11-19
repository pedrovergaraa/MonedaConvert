using CurrencyConvert.Models.Dtos;
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
        try
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var subscription = _subscriptionService.GetUserSubscription(userId);

            return Ok(new
            {
                subscription.Name,
                subscription.AllowedAttempts,
                subscription.Price
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }


    [HttpPost("change")]
    public IActionResult ChangeSubscription(ChangeSubscriptionDto dto)
    {
        if (dto == null || dto.SubscriptionId <= 0)
        {
            return BadRequest("Invalid subscription data provided.");
        }

        try
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"));
            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim not found.");
            }

            int userId = Int32.Parse(userIdClaim.Value);
            _subscriptionService.ChangeUserSubscription(userId, dto.SubscriptionId);

            return Ok("Subscription updated successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

}
