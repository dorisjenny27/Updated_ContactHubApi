using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ContactHub.Model.Entity
{
    public class User : IdentityUser
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
         
        
        [MaxLength(256)]
        public string? PhotoId { get; set; } 
        
        [MaxLength(256)]
        public string? PhotoUrl { get; set; } 
    }
}
