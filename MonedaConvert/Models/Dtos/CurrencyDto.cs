using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonedaConvert.Models.Dtos
{
    public class CurrencyDto
    {
        public int Id { get; set; } 

        public string? Legend { get; set; }

        public string? Symbol { get; set; }

        public decimal? IC { get; set; }


    }
}
