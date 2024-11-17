using System.ComponentModel.DataAnnotations;

namespace CurrencyConvert.Models.Dtos
{
    public class ChangeSubscriptionDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid subscription ID.")]
        public int SubscriptionId { get; set; }
    }
}
