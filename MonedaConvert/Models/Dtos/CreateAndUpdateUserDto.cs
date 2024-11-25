using System.ComponentModel.DataAnnotations;

namespace CurrencyConvert.Models.Dtos
{
    public class CreateAndUpdateUserDto
    {

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(4, ErrorMessage = "La contraseña debe tener al menos 4 caracteres")]
        public string Password { get; set; }

        public int? SubscriptionId { get; set; } = 1;
    }

}
