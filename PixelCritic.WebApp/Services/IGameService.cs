using PixelCritic.WebApp.Models;

namespace PixelCritic.WebApp.Services
{
    public interface IGameService
    {
        public Task<List<Game>> GetGamesAsync(string query);
        public Task<string> CallApiAsync(string query);
    }
}
