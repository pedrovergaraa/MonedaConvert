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

        public int Attempts { get; set; } 

        [ForeignKey("SubscriptionId")]
        public Subscription? Subscription { get; set; }

        public int? SubscriptionId { get; set; } = 1;

        public List<Currency>? Currencies { get; set; }

        public ICollection<FavoriteCurrency> FavoriteCurrencies { get; set; } = new List<FavoriteCurrency>();


    }
}
