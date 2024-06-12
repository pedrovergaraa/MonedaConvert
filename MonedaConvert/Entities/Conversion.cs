namespace MonedaConvert.Entities
{
    public class Conversion
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FromCurrencyCode { get; set; }
        public string ToCurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public DateTime ConversionDate { get; set; }

        public User User { get; set; }
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
    }
}
