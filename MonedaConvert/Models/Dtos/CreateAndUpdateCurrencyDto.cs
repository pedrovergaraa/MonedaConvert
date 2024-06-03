namespace MonedaConvert.Models.Dtos
{
    public class CreateAndUpdateCurrencyDto
    {
        public int? Id { get; set; } 
        public string? Symbol { get; set; }    
        public string? Legend { get; set; } 
        public decimal ? IC { get; set; }

    }
}
