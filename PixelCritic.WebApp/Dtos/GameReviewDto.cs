using PixelCritic.WebApp.Models;

namespace PixelCritic.WebApp.Dtos
{
    // https://learn.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
    public class GameReviewDto
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string GameUrl { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
        public string FreetogameProfileUrl { get; set; } = string.Empty;
        public int Rating { get; set; }

        public int NumOfReviews { get; set; }
        public IEnumerable<UserReviewDto> Reviews { get; set; } = new List<UserReviewDto>();
        public static GameReviewDto Map(Game game, IEnumerable<(User, Review)> userReviews)
        {
            var reviews = UserReviewDto.Map(userReviews);
            var reviewCount = reviews.Count();
            var rating = reviewCount > 0 
                ? ((double)reviews.Sum(r => r.Review.Score) / reviewCount)
                : 0;

            return new GameReviewDto
            {
                Id = game.Id,
                Title = game.Title,
                Thumbnail = game.Thumbnail,
                ShortDescription = game.ShortDescription,
                GameUrl = game.GameUrl,
                Genre = game.Genre,
                Platform = game.Platform,
                Publisher = game.Publisher,
                Developer = game.Developer,
                ReleaseDate = game.ReleaseDate,
                FreetogameProfileUrl = game.FreetogameProfileUrl,
                Reviews = reviews,
                NumOfReviews = reviewCount,
                Rating = (int)Math.Round(rating, MidpointRounding.AwayFromZero)
            };
        }
    }
}
