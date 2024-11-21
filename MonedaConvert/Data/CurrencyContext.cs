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

            {
                Currency pesoArgentino = new Currency()
                {
                    CurrencyId = 1,
                    Legend = "ARS",
                    Symbol = "$",
                    IC = 0.005f
                };
                Currency USDollar = new Currency()
                {
                    CurrencyId = 2,
                    Legend = "USD",
                    Symbol = "$",
                    IC = 1f
                };
                Currency euro = new Currency()
                {
                    CurrencyId = 3,
                    Legend = "EUR",
                    Symbol = "€",
                    IC = 1.1f
                };
                Currency britishPound = new Currency()
                {
                    CurrencyId = 4,
                    Legend = "GBP",
                    Symbol = "£",
                    IC = 1.3f
                };
                Currency japaneseYen = new Currency()
                {
                    CurrencyId = 5,
                    Legend = "JPY",
                    Symbol = "¥",
                    IC = 0.007f
                };
                Currency canadianDollar = new Currency()
                {
                    CurrencyId = 6,
                    Legend = "CAD",
                    Symbol = "$",
                    IC = 0.75f
                };
                Currency australianDollar = new Currency()
                {
                    CurrencyId = 7,
                    Legend = "AUD",
                    Symbol = "$",
                    IC = 0.72f
                };
                Currency swissFranc = new Currency()
                {
                    CurrencyId = 8,
                    Legend = "CHF",
                    Symbol = "$",
                    IC = 1.05f
                };


                Subscription Free = new Subscription()
                {
                    SubId = 1,
                    Name = "Free",
                    AllowedAttempts = 10,
                    Price = 0
                };
                Subscription Trial = new Subscription()
                {
                    SubId = 2,
                    Name = "Trial",
                    AllowedAttempts = 100,
                    Price = 10
                };
                Subscription Premium = new Subscription()
                {
                    SubId = 3,
                    Name = "Premium",
                    AllowedAttempts = int.MaxValue,
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
