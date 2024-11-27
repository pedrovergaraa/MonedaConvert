namespace CurrencyConvert.Models.Dtos
{
    public class CreateAndUpdateCurrencyDto
    {
        public string? Symbol { get; set; }    
        public string? Legend { get; set; } 
        public float  IC { get; set; }

    }
}
