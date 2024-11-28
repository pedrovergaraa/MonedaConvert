using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConvert.Entities
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubId { get; set; }

        [Required] 
        public string Name { get; set; } = string.Empty;
        public int Conversions { get; set; }

        public decimal Price { get; set; }


    }
}
