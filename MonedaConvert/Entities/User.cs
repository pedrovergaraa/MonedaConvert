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
        public List<Currency>? Coins { get; set; }
        public State State { get; set; } = State.Active;
        public Role Role { get; set; } = Role.User;
        [ForeignKey("SuscripcionId")]
        public Suscription Sub { get; set; }
        public int SuscripcionId { get; set; }
    }
}
