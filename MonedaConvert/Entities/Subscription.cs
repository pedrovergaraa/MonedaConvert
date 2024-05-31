using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonedaConvert.Entities
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; } 

        public string Name { get; set; }  
        //Free, Trial, Pro

        public int Tries { get; set; }
        //Los tries son para la sub free, maximo 10
    }
}
