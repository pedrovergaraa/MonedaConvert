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

    public Subscription GetUserSubscription(int userId)
    {
        if (userId <= 0)
        {
            throw new Exception("Invalid userId.");
        }

        var user = _context.Users
            .Include(u => u.Subscription)
            .FirstOrDefault(u => u.UserId == userId);

        if (user?.Subscription == null)
        {
            throw new Exception("User or subscription not found.");
        }

        return user.Subscription;
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
