using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonedaConvert.Entities
{
    public class UserCurrency
{
		[ForeignKey("UserId")]
        public int userId {  get; set; }
	    public User user { get; set; }
		[ForeignKey("CurrencyId")]
		public int currencyId { get; set; }
	    public Currency currency { get; set; }
        public decimal Count { get; set; }
    }
}


