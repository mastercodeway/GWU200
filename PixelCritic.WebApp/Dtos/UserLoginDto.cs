using PixelCritic.WebApp.Models;

namespace PixelCritic.WebApp.Dtos
{
    public class UserLoginDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public static UserLoginDto? MapToDto(User? user)
        {
            if (user == null) return null;

            return new UserLoginDto
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };
        }
    }
}
