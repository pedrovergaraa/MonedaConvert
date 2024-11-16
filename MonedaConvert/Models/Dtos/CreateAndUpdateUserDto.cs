namespace CurrencyConvert.Models.Dtos
{
    public class CreateAndUpdateUserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? SubscriptionId { get; set; } // Campo nullable
    }
}
