using MonedaConvert.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonedaConvert.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int totalConversions { get; set; }
        public Subscription Subscription { get; set; }

        public List<Currency>? Currencies { get; set; }
        public List<Conversion>? ConversionHistories { get; set; }

        
    }
}

