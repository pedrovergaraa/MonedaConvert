using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonedaConvert.Entities
{
    public class UserCurrency
{
        public int UserId {  get; set; }
	    public User User { get; set; }
		
		public int CurrencyId { get; set; }
	    public Currency Currency { get; set; }
    }
}


