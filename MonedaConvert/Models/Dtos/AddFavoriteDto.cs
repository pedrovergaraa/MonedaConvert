namespace CurrencyConvert.Models.Dtos;

public class AddFavoriteDto
{
    public int CurrencyId { get; set; }
    public string? Legend { get; set; }
    public string? Symbol { get; set; }
    public float IC { get; set; }
}