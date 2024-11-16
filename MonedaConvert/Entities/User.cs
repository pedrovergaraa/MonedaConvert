using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConvert.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int? TotalConversions { get; set; } = 0;

        [ForeignKey("SubscriptionId")]
        public Subscription? Subscription { get; set; }

        public int? SubscriptionId { get; set; } = 1;

        public List<Currency>? Currencies { get; set; }

        // Nueva propiedad para las monedas favoritas
        public List<FavoriteCurrency>? FavoriteCurrencies { get; set; }
    }
}
