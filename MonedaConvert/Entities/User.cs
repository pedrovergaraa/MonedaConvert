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
        public string? Email { get; set; }
        public string? Password { get; set; }

        public List<UserCurrency>? UserCurrency { get; set; } = new List<UserCurrency> { };
        public List<Favorites>? Favorites { get; set; } = new List<Favorites> { };

        public State State { get; set; } = State.Active;
        public Role Role { get; set; } = Role.User;
        [ForeignKey("SubscriptionId")]
        public Subscription Sub { get; set; }
        public int SubscriptionId { get; set; }
    }
}
