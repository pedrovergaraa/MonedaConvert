using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonedaConvert.Entities
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string? Legend { get; set; }

        public string? Symbol { get; set; }
        //Simbolo grafico
        public decimal? IC { get; set; }
        //Indice de convertibilidad
        //El índice de convertibilidad será la
        //relación que existe entre una moneda y el dólar americano expresada en cuanto vale
        //una unidad de dicha moneda en comparación a 1 usd.
    }
}
