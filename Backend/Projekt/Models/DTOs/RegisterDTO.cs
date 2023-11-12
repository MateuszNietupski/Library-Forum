using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
