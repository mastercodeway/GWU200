using PixelCritic.WebApp.Models;
using PixelCritic.WebApp.Repositories.Shared;

namespace PixelCritic.WebApp.Repositories
{
    public class ReviewRepo : BaseRepo<Review, Guid>, IReviewRepo
    {
        private readonly PixelDbContext _context;
        public ReviewRepo(PixelDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
