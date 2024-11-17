namespace CurrencyConvert.Models.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int? SubscriptionId { get; set; }
        public List<CurrencyDto>? Currencies { get; set; }
        public List<AddFavoriteDto>? FavoriteCurrencies { get; set; }
    }

}
