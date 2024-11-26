using PixelCritic.WebApp.Repositories;

namespace PixelCritic.WebApp.Repositories
{
    //https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PixelDbContext _context;
        public IArticleRepo ArticleRepo { get; }

        public IReviewRepo ReviewRepo { get; }

        public IUserRepo UserRepo { get; }

        public IRatedGameRepo RatedGameRepo { get; }

        public UnitOfWork(PixelDbContext context)
        {
            _context = context;
            UserRepo = new UserRepo(context);
            ArticleRepo = new ArticleRepo(context);
            ReviewRepo = new ReviewRepo(context);
            RatedGameRepo = new RatedGameRepo(context); 
        }

        public int Save() => _context.SaveChanges();
        public Task<int> SaveAsync() => _context.SaveChangesAsync();
        public void ChangeTrackerDetectChanges() => _context.ChangeTracker.DetectChanges();
        public void ClearChangeTracker() => _context.ChangeTracker.Clear();
        //GC.suppressFinalize Source: https://www.c-sharpcorner.com/article/implementing-unit-of-work-and-repository-pattern-with-dependency-injection-in-n/
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
