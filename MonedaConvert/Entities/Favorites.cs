using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonedaConvert.Entities
{
    public class Favorites
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

    }
}

