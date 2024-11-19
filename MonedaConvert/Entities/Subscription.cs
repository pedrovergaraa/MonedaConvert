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
        public int AllowedAttempts { get; set; }

        public decimal Price { get; set; }

        public List<User>? Users { get; set; } = new List<User>();

    }
}
