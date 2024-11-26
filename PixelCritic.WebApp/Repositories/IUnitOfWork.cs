namespace PixelCritic.WebApp.Repositories
{
    public interface IUnitOfWork
    {
        IArticleRepo ArticleRepo { get; }
        IReviewRepo ReviewRepo { get; }
        IUserRepo UserRepo { get; }
        IRatedGameRepo RatedGameRepo { get; }

        int Save();
        Task<int> SaveAsync();
        void ChangeTrackerDetectChanges();
        void ClearChangeTracker();
        void Dispose();
    }
}
