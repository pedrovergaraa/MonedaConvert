using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonedaConvert.Entities
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubId { get; set; }

        public string? Name { get; set; }

        public long Convertions { get; set; }

        public int Price { get; set; }

        public List<User>? Users { get; set; }

    }
}
