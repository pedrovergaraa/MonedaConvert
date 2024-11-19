using CurrencyConvert.Data;
using CurrencyConvert.Entities;
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

    public void ChangeUserSubscription(int userId, int subscriptionId)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
        var subscription = _context.Subscriptions.Find(subscriptionId);

        if (user == null)
            throw new Exception("User not found.");

        if (subscription == null)
            throw new Exception("Subscription not found.");

        if (user.SubscriptionId == subscriptionId)
            throw new Exception("User already has this subscription.");

        // Cambiar la suscripción
        user.SubscriptionId = subscriptionId;
        _context.SaveChanges();
    }

}
