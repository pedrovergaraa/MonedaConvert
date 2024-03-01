using Microsoft.EntityFrameworkCore;
using MonedaConvert.Entities;

namespace MonedaConvert.Data
{
    public class MonedaContext : DbContext
    {
        public DbSet<User>Users { get; set; }
        public DbSet<Currency> Coins { get; set; }

        public MonedaContext(DbContextOptions<MonedaContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {

        }

    }
}
