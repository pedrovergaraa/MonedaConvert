using CurrencyConvert.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class FavoriteCurrency
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FavoriteCurrencyId { get; set; }

    [ForeignKey("CurrencyId")]
    public Currency? Currency { get; set; }
    public int CurrencyId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
    public int? UserId { get; set; }
}

