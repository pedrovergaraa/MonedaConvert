using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonedaConvert.Models.Dtos
{
    public class CurrencyDto
    {
        public int Id { get; set; } 

        public string? Legend { get; set; }
<<<<<<< HEAD

=======
>>>>>>> e2bdb655fa3a6cb462f9a5ff3f22a671aaf579f8

        public string? Symbol { get; set; }

        public decimal? IC { get; set; }


    }
}
