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

    public List<Subscription> GetAllSubscriptions()
    {
        return _context.Subscriptions.ToList();
    }

    public Subscription GetUserSubscription(int userId)
    {
        var user = _context.Users
            .Where(u => u.UserId == userId)
            .Select(u => u.Subscription) // Evitar cargar al usuario completo si no es necesario
            .FirstOrDefault();

        if (user == null)
            throw new Exception("User not found.");

        return user ?? throw new Exception("User does not have an active subscription.");
    }

    public void UpdateUserSubscription(int userId, ActivateSubscriptionDto dto)
    {
        // Buscar al usuario en la base de datos
        var user = _context.Users.Find(userId);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        // Buscar la nueva suscripción
        var newSubscription = _context.Subscriptions.Find(dto.NewSubscriptionId);
        if (newSubscription == null)
        {
            throw new Exception("Subscription not found.");
        }

        // Validar si ya tiene la misma suscripción
        if (user.SubscriptionId == newSubscription.SubId)
        {
            throw new Exception("User already has this subscription.");
        }

        // Actualizar la suscripción del usuario
        user.SubscriptionId = newSubscription.SubId;
        user.Attempts = newSubscription.Conversions; 
        _context.SaveChanges();
    }


}
