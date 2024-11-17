using CurrencyConvert.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConvert.Data
{
    public class CurrencyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<FavoriteCurrency> FavoriteCurrencies { get; set; }

        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones
            modelBuilder.Entity<User>()
                .HasOne(u => u.Subscription)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.SubscriptionId);

            modelBuilder.Entity<Currency>()
                .HasOne(c => c.User)
                .WithMany(u => u.Currencies)
                .HasForeignKey(c => c.UserId);

            //modelBuilder.Entity<Currency>()
            //     .HasIndex(c => new { c.Legend, c.UserId })
            //     .IsUnique();

            modelBuilder.Entity<FavoriteCurrency>()
                .HasOne(fc => fc.User)
                .WithMany(u => u.FavoriteCurrencies)
                .HasForeignKey(fc => fc.UserId);

            // Valores predeterminados
            modelBuilder.Entity<User>()
                .Property(u => u.SubscriptionId)
                .HasDefaultValue(1);

            // Suscripciones iniciales
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription
                {
                    SubId = 1,
                    Name = "Free",
                    Conversions = 10,
                    Price = 0
                },
                new Subscription
                {
                    SubId = 2,
                    Name = "Trial",
                    Conversions = 100,
                    Price = 10
                },
                new Subscription
                {
                    SubId = 3,
                    Name = "Premium",
                    Conversions = int.MaxValue,
                    Price = 15
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
