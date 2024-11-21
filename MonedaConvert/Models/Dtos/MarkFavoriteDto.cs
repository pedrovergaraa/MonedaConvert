using System.ComponentModel.DataAnnotations;

namespace CurrencyConvert.Models.Dtos;

public class MarkFavoriteDto
{
    [Required]
    public int CurrencyId { get; set; }
}