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
                .HasForeignKey(u => u.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict); // Evita la eliminación en cascada accidental

            modelBuilder.Entity<Currency>()
                .HasOne(c => c.User)
                .WithMany(u => u.Currencies)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FavoriteCurrency>()
                .HasOne(fc => fc.User)
                .WithMany(u => u.FavoriteCurrencies)
                .HasForeignKey(fc => fc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FavoriteCurrency>()
                .HasOne(fc => fc.Currency)
                .WithMany()
                .HasForeignKey(fc => fc.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Datos iniciales para la tabla de monedas
            modelBuilder.Entity<Currency>().HasData(
             new Currency { CurrencyId = 1, Legend = "USD", Symbol = "$", IC = 1.0f, UserId = null, IsDefault = true },
             new Currency { CurrencyId = 2, Legend = "EUR", Symbol = "€", IC = 1.09f, UserId = null, IsDefault = false },
             new Currency { CurrencyId = 3, Legend = "GBP", Symbol = "£", IC = 0.8f, UserId = null, IsDefault = false },
             new Currency { CurrencyId = 4, Legend = "JPY", Symbol = "¥", IC = 110.0f, UserId = null, IsDefault = false },
             new Currency { CurrencyId = 5, Legend = "KC", Symbol = "kc", IC = 0.043f, UserId = null, IsDefault = false },
             new Currency { CurrencyId = 6, Legend = "ARS", Symbol = "$", IC = 0.002f, UserId = null, IsDefault = false }
 );


            // Datos iniciales para la tabla de suscripciones
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription
                {
                    SubId = 1,
                    Name = "Free",
                    AllowedAttempts = 10,
                    Price = 0
                },
                new Subscription
                {
                    SubId = 2,
                    Name = "Trial",
                    AllowedAttempts = 100,
                    Price = 10
                },
                new Subscription
                {
                    SubId = 3,
                    Name = "Premium",
                    AllowedAttempts = int.MaxValue,
                    Price = 15
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
