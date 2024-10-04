namespace CurrencyConvert.Models.Dtos
{
    public class CreateAndUpdateCurrencyDto
    {
        public int? Id { get; set; } 
        public string? Symbol { get; set; }    
        public string? Legend { get; set; } 
        public float ? IC { get; set; }

    }
}
