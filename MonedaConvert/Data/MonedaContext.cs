using Microsoft.EntityFrameworkCore;
using MonedaConvert.Entities;

namespace MonedaConvert.Data
{
    public class MonedaContext : DbContext
    {
        public MonedaContext(DbContextOptions<MonedaContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {

        }
        public DbSet<User>Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Conversion> ConversionHistories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Currency pesoArgentino = new Currency
            {
                Id = 1,
                Symbol = "Peso Argentino",
                Legend = "ARS$",
                IC = 0.002
            };
            Currency monedaAmericana = new Currency
            {
                Id = 2,
                Symbol = "Dolar Americano",
                Legend = "USD$",
                IC = 1
            };
            Currency coronaCheca = new Currency
            {
                Id = 3,
                Symbol = "Corona Checa",
                Legend = "KC$",
                IC = 0.043
            };
            Currency euro = new Currency
            {
                Id = 4,
                Symbol = "Euro",
                Legend = "EUR$",
                IC = 1.09
            };
            Currency libra = new Currency
            {
                Id = 5,
                Symbol = "Libra Esterlina",
                Legend = "GBP$",
                IC = 1.42
            };

            modelBuilder.Entity<Currency>().HasData(
                pesoArgentino, monedaAmericana, coronaCheca, euro, libra);

            base.OnModelCreating(modelBuilder);
        }

    }
}
