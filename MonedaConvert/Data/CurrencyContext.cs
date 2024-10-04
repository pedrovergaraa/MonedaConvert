using Microsoft.EntityFrameworkCore;
using CurrencyConvert.Entities;

namespace CurrencyConvert.Data
{
    public class CurrencyContext : DbContext
    {
        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {

        }
        public DbSet<User>Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Currency> Currencies { get; set; }


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
                Convertions = 10,
                Price = 0

            };


            Subscription sub2 = new Subscription()
            {
                SubId = 2,
                Name = "Trial",
                Convertions = 100,
                Price = 7

            };


            Subscription sub3 = new Subscription()
            {
                SubId = 3,
                Name = "Premium",
                Convertions = 999999999999999999,
                Price = 10

            };

            modelBuilder.Entity<Subscription>()
             .HasData(sub1, sub2, sub3);

        }



    }
}
