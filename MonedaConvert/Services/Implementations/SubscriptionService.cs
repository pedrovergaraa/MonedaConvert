using CurrencyConvert.Data;
using CurrencyConvert.Entities;
using CurrencyConvert.Models.Dtos;
using Microsoft.EntityFrameworkCore;

public class SubscriptionService
{
    private readonly CurrencyContext _context;

    public SubscriptionService(CurrencyContext context)
    {
        _context = context;
    }

    // Obtener todas las suscripciones
    public List<Subscription> GetAllSubscriptions()
    {
        return _context.Subscriptions.AsNoTracking().ToList();
    }

    // Obtener la suscripción de un usuario
    public Subscription GetUserSubscription(int userId)
    {
        var subscription = _context.Users
            .Include(u => u.Subscription) // Cargar la suscripción relacionada
            .Where(u => u.UserId == userId)
            .Select(u => u.Subscription)
            .FirstOrDefault();

        if (subscription == null)
        {
            throw new Exception("User does not have an active subscription.");
        }

        return subscription;
    }

    // Actualizar la suscripción de un usuario
    public void UpdateUserSubscription(int userId, ActivateSubscriptionDto dto)
    {
        var user = _context.Users.Include(u => u.Subscription).FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        var newSubscription = _context.Subscriptions.Find(dto.NewSubscriptionId);
        if (newSubscription == null)
        {
            throw new Exception("Subscription not found.");
        }

        if (user.SubscriptionId == newSubscription.SubId)
        {
            throw new Exception("User already has this subscription.");
        }

        user.SubscriptionId = newSubscription.SubId;
        user.Attempts = newSubscription.Conversions;

        _context.SaveChanges();
    }
}
