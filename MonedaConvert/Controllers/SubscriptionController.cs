﻿using CurrencyConvert.Models.Dtos;
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

    // Obtener la suscripción actual del usuario
    [HttpGet("userSub")]
    public IActionResult GetUserSubscription()
    {
        try
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            var subscription = _subscriptionService.GetUserSubscription(userId);

            return Ok(new
            {
                subscription.Name,
                subscription.Conversions,
                subscription.Price
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    // Cambiar la suscripción de un usuario
    [HttpPost("change")]
    public IActionResult ChangeSubscription(ChangeSubscriptionDto dto)
    {
        if (dto == null || dto.SubscriptionId <= 0)
        {
            return BadRequest("Invalid subscription data provided.");
        }

        try
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            _subscriptionService.ChangeUserSubscription(userId, dto.SubscriptionId);

            return Ok("Subscription updated successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }
}