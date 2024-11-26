using PixelCritic.WebApp.Dtos;
using System.ComponentModel.DataAnnotations;

namespace PixelCritic.WebApp.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        
    }
}
