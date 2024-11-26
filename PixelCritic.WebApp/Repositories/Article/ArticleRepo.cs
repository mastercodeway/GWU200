using PixelCritic.WebApp.Models;
using PixelCritic.WebApp.Repositories.Shared;

namespace PixelCritic.WebApp.Repositories
{
    public class ArticleRepo : BaseRepo<Article, Guid>, IArticleRepo
    {
        private readonly PixelDbContext _context;
        public ArticleRepo(PixelDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
