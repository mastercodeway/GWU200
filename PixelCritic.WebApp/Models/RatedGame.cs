using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PixelCritic.WebApp.Models
{
    public class RatedGame
    {

        [Key]
        [property: JsonProperty("id")]
        public int Id { get; set; }
        [property: JsonProperty("title")]
        public string Title { get; set; } = string.Empty;
        [property: JsonProperty("thumbnail")]
        public string Thumbnail { get; set; } = string.Empty;
        [property: JsonProperty("short_description")]
        public string ShortDescription { get; set; } = string.Empty;
        [property: JsonProperty("game_url")]
        public string GameUrl { get; set; } = string.Empty;
        [property: JsonProperty("genre")]
        public string Genre { get; set; } = string.Empty;
        [property: JsonProperty("platform")]
        public string Platform { get; set; } = string.Empty;
        [property: JsonProperty("publisher")]
        public string Publisher { get; set; } = string.Empty;
        [property: JsonProperty("developer")]
        public string Developer { get; set; } = string.Empty;
        [property: JsonProperty("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;
        [property: JsonProperty("freetogame_profile_url")]
        public string FreetoGameUrl { get; set; } = string.Empty;
        [property: JsonProperty("rating")]
        public int Rating { get; set; }
        [property: JsonProperty("number_of_reviews")]
        public int NumOfReviews { get; set; }

        public static RatedGame MapToRatedGame(Game game)
        {

            return new RatedGame
            {
                Id = (int)game.Id,
                Title = game.Title,
                Thumbnail = game.Thumbnail,
                ShortDescription = game.ShortDescription,
                GameUrl = game.GameUrl,
                Genre = game.Genre,
                Platform = game.Platform,
                Publisher = game.Publisher,
                Developer = game.Developer,
                ReleaseDate = game.ReleaseDate,
                FreetoGameUrl = game.FreetogameProfileUrl,
                Rating = 0, 
                NumOfReviews = 0,
            };
        }
        public static IEnumerable<RatedGame> MapToRatedGame(IEnumerable<Game> games)
        {
            var result = new List<RatedGame>();
            foreach (var game in games)
            {
               result.Add(RatedGame.MapToRatedGame(game));
            }
            return result;
        }
    }
}
