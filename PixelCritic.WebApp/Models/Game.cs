using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace PixelCritic.WebApp.Models
{

    public record Game(
        [property: JsonProperty("id")] int? Id,
        [property: JsonProperty("title")] string Title,
        [property: JsonProperty("thumbnail")] string Thumbnail,
        [property: JsonProperty("short_description")] string ShortDescription,
        [property: JsonProperty("game_url")] string GameUrl,
        [property: JsonProperty("genre")] string Genre,
        [property: JsonProperty("platform")] string Platform,
        [property: JsonProperty("publisher")] string Publisher,
        [property: JsonProperty("developer")] string Developer,
        [property: JsonProperty("release_date")] string ReleaseDate,
        [property: JsonProperty("freetogame_profile_url")] string FreetogameProfileUrl
    );
}
