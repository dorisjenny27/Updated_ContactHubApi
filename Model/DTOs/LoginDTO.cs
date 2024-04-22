using System.ComponentModel.DataAnnotations;

namespace ContactHub.Model.DTOs
{
    public class LoginDTO
    {      
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}

