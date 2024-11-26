using PixelCritic.WebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PixelCritic.WebApp.Dtos
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public string Titel { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Score { get; set; }
        public DateOnly Posted { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int GameId { get; set; }
        public Guid UserRefId { get; set; }
        public static ReviewDto Map(Review review)
            => new ReviewDto 
            { 
                Id = review.Id,
                Titel = review.Titel,
                Description = review.Description,
                Score = review.Score,
                Posted = review.Posted,
                GameId = review.GameId,
                UserRefId = review.UserRefId
            };

        public static IEnumerable<ReviewDto> Map(IEnumerable<Review> reviews)
        {
            var result = new List<ReviewDto>();
            foreach (var review in reviews)
            {
                result.Add(Map(review));
            }
            return result;
        }
    }
}
