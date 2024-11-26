using PixelCritic.WebApp.Models;
using PixelCritic.WebApp.Services;

namespace PixelCritic.WebApp.Dtos
{
    public class UserRegisterDto
    {
        
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        
    }
}
