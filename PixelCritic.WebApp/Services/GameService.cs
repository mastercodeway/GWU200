using Newtonsoft.Json;
using PixelCritic.WebApp.Dtos;
using PixelCritic.WebApp.Models;

namespace PixelCritic.WebApp.Services
{
    // https://learn.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
    public class GameService : IGameService
    {
        // Url för anrop av api 
        private const string BASE_URL = "https://www.freetogame.com/api/games";
        private readonly HttpClient _Client;

        public GameService(HttpClient client)
        {
            _Client = client;
        }
        // hanterar och deseriliserar json
        public async Task<List<Game>> GetGamesAsync(string query)
        {
            var jsonRespons = await CallApiAsync(query);
            return JsonConvert.DeserializeObject<List<Game>>(jsonRespons)??new List<Game>();
        }
        // anropar api och hämtar data error hanterar om det skett något på vägen. 
        public async Task<string> CallApiAsync(string query)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, BASE_URL + query);
                var response = await _Client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }

        }

       
    }
}
