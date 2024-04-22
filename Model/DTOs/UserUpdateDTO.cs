using System.ComponentModel.DataAnnotations;

namespace ContactHub.Model.DTOs
{
    public class UserUpdateDTO
    {
            [RegularExpression(@"234-[0-9]{10}")]
            public string? PhoneNumber { get; set; }      
    }
}
