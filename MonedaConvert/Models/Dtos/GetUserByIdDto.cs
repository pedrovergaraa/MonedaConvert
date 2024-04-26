using MonedaConvert.Models.Enum;

namespace MonedaConvert.Models.Dtos
{
    public class GetUserByIdDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public State State { get; set; }
        public Role Role { get; set; }
    }
}
