using System.ComponentModel.DataAnnotations;

namespace ContactHub.Model.DTOs
{
    public class UserToAddDTO
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = "";
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = "";
        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = "";
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";

        [RegularExpression(@"234-[0-9]{10}")]
        public string? PhoneNumber { get; set; }
    }
}
