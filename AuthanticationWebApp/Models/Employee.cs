using System.ComponentModel.DataAnnotations;

namespace AuthanticationWebApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Family { get; set; }
    }
}
