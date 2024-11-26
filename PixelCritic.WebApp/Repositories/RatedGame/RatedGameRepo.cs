using PixelCritic.WebApp.Models;
using PixelCritic.WebApp.Repositories.Shared;

namespace PixelCritic.WebApp.Repositories
{
    public class RatedGameRepo : BaseRepo<RatedGame, int>, IRatedGameRepo
    {
        private readonly PixelDbContext _context;
        public RatedGameRepo(PixelDbContext context) : base(context)
        {
            _context = context;
        }
    }

}
