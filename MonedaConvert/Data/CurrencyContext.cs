using CurrencyConvert.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConvert.Data
{
    public class CurrencyContext : DbContext
    {
        public DbSet<User>Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<FavoriteCurrency> FavoriteCurrencies { get; set; }

        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
             .HasOne<Subscription>(u => u.Subscription).
             WithMany(c => c.Users);

            modelBuilder.Entity<Currency>()
                .HasOne<User>(u => u.User)
                .WithMany(c => c.Currencies);

            Subscription sub1 = new Subscription()
            {
                SubId = 1,
                Name = "Free",
                Conversions = 10,
                Price = 0

            };


            Subscription sub2 = new Subscription()
            {
                SubId = 2,
                Name = "Trial",
                Conversions = 100,
                Price = 10

            };


            Subscription sub3 = new Subscription()
            {
                SubId = 3,
                Name = "Premium",
                Conversions = 999999999999999999,
                Price = 15

            };

            modelBuilder.Entity<Subscription>()
             .HasData(sub1, sub2, sub3);

            base.OnModelCreating(modelBuilder);
        }

    }
}
