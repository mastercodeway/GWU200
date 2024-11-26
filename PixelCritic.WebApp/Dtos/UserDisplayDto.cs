using PixelCritic.WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace PixelCritic.WebApp.Dtos
{
    public class UserDisplayDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public static UserDisplayDto Map(User user)
        {
            return new UserDisplayDto
            {
                Id = user.Id,
                Username = user.Username
            };
        }
        public static IEnumerable<UserDisplayDto> Map(IEnumerable<User> users)
        {
            var result = new List<UserDisplayDto>();
            foreach (var user in users)
            {
                result.Add(Map(user));
            }
            return result;
        }
    }
}

