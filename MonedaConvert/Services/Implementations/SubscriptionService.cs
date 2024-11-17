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

    // Obtener todas las suscripciones disponibles
    public List<Subscription> GetAllSubscriptions()
    {
        try
        {
            return _context.Subscriptions.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching subscriptions", ex);
        }
    }

    // Obtener la suscripción actual de un usuario
    public Subscription GetUserSubscription(int userId)
    {
        try
        {
            var user = _context.Users.Include(u => u.Subscription)
                                      .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.Subscription == null)
            {
                throw new Exception("User does not have an active subscription");
            }

            return user.Subscription;
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching user subscription", ex);
        }
    }

    // Cambiar la suscripción de un usuario
    public void ChangeUserSubscription(int userId, int subscriptionId)
    {
        try
        {
            var user = _context.Users.Include(u => u.Subscription)
                                      .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found");
            }

            var subscription = _context.Subscriptions.FirstOrDefault(s => s.SubId == subscriptionId);

            if (subscription == null)
            {
                throw new Exception($"Subscription with ID {subscriptionId} not found");
            }

            // Validar si el usuario ya tiene esta suscripción
            if (user.Subscription?.SubId == subscriptionId)
            {
                throw new Exception("The user already has this subscription");
            }

            // Asignar la nueva suscripción
            user.Subscription = subscription;
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Se puede agregar un log aquí si es necesario
            throw new Exception("Error changing user subscription", ex);
        }
    }
}
