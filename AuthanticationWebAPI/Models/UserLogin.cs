using System.ComponentModel.DataAnnotations;

namespace AuthanticationWebAPI.Models
{
    public class UserLogin
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
