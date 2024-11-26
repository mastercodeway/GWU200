using PixelCritic.WebApp.Dtos;

namespace PixelCritic.WebApp.ViewModels
{
    public class GameVM
    {
        public required GameReviewDto GameReviews { get; set; }
        public ReviewDto NewReview { get; set; }
    }
}
