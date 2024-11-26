using PixelCritic.WebApp.Models;

namespace PixelCritic.WebApp.Dtos
{
    public class UserReviewDto
    {
        
        public ReviewDto Review { get; set; }
        public UserDisplayDto User { get; set; }

        public static UserReviewDto Map((User user, Review review) userReview)
        {
            return new UserReviewDto
            {
                User = UserDisplayDto.Map(userReview.user),
                Review = ReviewDto.Map(userReview.review)
           }; 
        }
        public static IEnumerable<UserReviewDto> Map(IEnumerable<(User, Review)> userReviews)
        {
            var result = new List<UserReviewDto>();
            foreach (var userReview in userReviews)
            {
                result.Add(UserReviewDto.Map(userReview));
            }
            return result;
        }
    }
}
