namespace CurrencyConvert.Models.Dtos
{
    public class ConversionDto
    {
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public float Amount { get; set; }
    }

}

