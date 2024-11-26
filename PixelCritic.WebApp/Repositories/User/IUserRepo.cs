
using PixelCritic.WebApp.Dtos;
using PixelCritic.WebApp.Models;
using PixelCritic.WebApp.Repositories.Shared;

namespace PixelCritic.WebApp.Repositories
{
    public interface IUserRepo : IBaseRepo<User,Guid>
    {
        public Task<bool> AddUserAsync(User user);
        public Task<UserLoginDto?> GetUSerAsync(string Username);
    }
}
