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
        return _context.Subscriptions.AsNoTracking().ToList();
    }

    public UserSubscriptionDto GetUserSubscription(int userId)
    {
        // Busca al usuario por su ID e incluye la relación con Subscription
        var user = _context.Users
            .Include(u => u.Subscription)
            .FirstOrDefault(u => u.UserId == userId);

        // Verifica si el usuario existe
        if (user == null)
        {
            throw new KeyNotFoundException($"No se encontró un usuario con el ID {userId}.");
        }

        // Verifica si el usuario tiene una suscripción
        if (user.Subscription == null)
        {
            throw new InvalidOperationException($"El usuario con el ID {userId} no tiene una suscripción activa.");
        }

        // Mapea la suscripción al DTO
        return new UserSubscriptionDto
        {
            Name = user.Subscription.Name,
            Conversions = user.Subscription.Conversions,
            Price = user.Subscription.Price
        };
    }

    public void UpdateUserSubscription(int userId, ActivateSubscriptionDto dto)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
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
            user.Attempts = newSubscription.Conversions == -1 ? int.MaxValue : newSubscription.Conversions;

            _context.SaveChanges();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
