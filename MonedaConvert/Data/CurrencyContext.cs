using CurrencyConvert.Entities;

namespace CurrencyConvert.Data
{
    public class CurrencyContext : DbContext
    {
        public DbSet<User>Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<FavoriteCurrency> FavoriteCuerrencies { get; set; }

        public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Currency pesoArgentino = new Currency()
            {
                Id = 1,
                Legend = "Peso Argentino",
                Symbol = "Ars$",
                IC = 0.002
            };
            Currency dolarAmericano = new Currency()
            {
                Id = 2,
                Legend = "Dolar Americano",
                Symbol = "Usd$",
                IC = 1
            };
            Currency coronaCheca = new Currency()
            {
                Id = 3,
                Legend = "Corona Checa",
                Symbol = "Kc$",
                IC = 0.043
            };
            Currency euro = new Currency()
            {
                Id = 4,
                Legend = "Euro",
                Symbol = "Eur$",
                IC = 1.09
            };

            modelBuilder.Entity<Currency>().HasData(
               pesoArgentino, dolarAmericano, coronaCheca, euro);

            base.OnModelCreating(modelBuilder);
        }

    }
}
