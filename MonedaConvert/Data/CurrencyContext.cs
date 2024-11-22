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

                Currency australianDollar = new Currency()
                {
                    CurrencyId = 10,
                    Legend = "AUD",
                    Symbol = "$",
                    IC = 0.72f,
                    IsDefault = true
                };
                Currency swissFranc = new Currency()
                {
                    CurrencyId = 11,
                    Legend = "CHF",
                    Symbol = "$",
                    IC = 1.05f,
                    IsDefault = true
                };

            {
                Currency pesoArgentino = new Currency()
                {
                    CurrencyId = 12,
                    Legend = "ARS",
                    Symbol = "$",
                    IC = 0.005f,
                    IsDefault = true
                };
                Currency USDollar = new Currency()
                {
                    CurrencyId = 13,
                    Legend = "USD",
                    Symbol = "$",
                    IC = 1f,
                    IsDefault = true
                };
                Currency euro = new Currency()
                {
                    CurrencyId = 14,
                    Legend = "EUR",
                    Symbol = "€",
                    IC = 1.1f,
                    IsDefault = true
                };
                Currency britishPound = new Currency()
                {
                    CurrencyId = 15,
                    Legend = "GBP",
                    Symbol = "£",
                    IC = 1.3f,
                    IsDefault = true
                };
                Currency japaneseYen = new Currency()
                {
                    CurrencyId = 16,
                    Legend = "JPY",
                    Symbol = "¥",
                    IC = 0.007f,
                    IsDefault = true
                };
                Currency canadianDollar = new Currency()
                {
                    CurrencyId = 17,
                    Legend = "CAD",
                    Symbol = "$",
                    IC = 0.75f,
                    IsDefault = true
                };

                Subscription Free = new Subscription()
                {
                    SubId = 4,
                    Name = "Free",
                    Conversions = 10,
                    Price = 0
                };
                Subscription Trial = new Subscription()
                {
                    SubId = 5,
                    Name = "Trial",
                    Conversions = 100,
                    Price = 10
                };
                Subscription Premium = new Subscription()
                {
                    SubId = 6,
                    Name = "Premium",
                    Conversions = int.MaxValue,
                    Price = 15
                };


                


                modelBuilder.Entity<Currency>().HasData(
                   pesoArgentino, USDollar, euro, britishPound, japaneseYen, canadianDollar, australianDollar, swissFranc);
                modelBuilder.Entity<Subscription>().HasData(
              Free, Trial, Premium);

                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
