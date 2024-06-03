using System.ComponentModel.DataAnnotations;

namespace MonedaConvert.Models.Dtos
{
    public class AuthenticationRequestDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
