using PixelCritic.WebApp.Models;
using PixelCritic.WebApp.Repositories.Shared;

namespace PixelCritic.WebApp.Repositories
{
    public interface IReviewRepo : IBaseRepo<Review, Guid>
    {
    }
}
