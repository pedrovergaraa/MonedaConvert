using Microsoft.EntityFrameworkCore;
using MonedaConvert.Entities;

namespace MonedaConvert.Data
{
    public class MonedaContext : DbContext
    {
        public DbSet<User>Users { get; set; }
        public DbSet<Currency> Coins { get; set; }
        public DbSet<UserCurrency> UserCurrency { get; set; }   
        public DbSet<Favorites> Favorites { get; set; }

        public MonedaContext(DbContextOptions<MonedaContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Currency pesoArgentino = new Currency();
            {
                Id = 1,
                Sybbol = "Peso Argentino",
                Legend = "ARS$",
                IC = 0.002
            },
            Currency monedaAmericana = new Currency();
            {
                Id = 2,
                Sybbol = "Dolar Americano",
                Legend = "USD$",
                IC = 1
            },
            Currency coronaCheca = new Currency();
            {
                Id = 3,
                Sybbol = "Corona Checa",
                Legend = "KC$",
                IC = 0.043
            },
            Currency euro = new Currency();
            {
                Id = 4,
                Sybbol = "Euro",
                Legend = "EUR$",
                IC = 1.09
            },
            Currency libra = new Currency();
            {
                Id = 5,
                Sybbol = "Libra Esterlina",
                Legend = "GBP$",
                IC = 1.42
            };

            modelBuilder.Entity<Currency>().HasData(
            pesoArgentino, monedaAmericana, coronaCheca, euro, libra);

            base.OnModelCreating(modelBuilder);
        }
    }
}
