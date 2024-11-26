using Microsoft.EntityFrameworkCore.Query;
using PixelCritic.WebApp.Models;
using System.Linq.Expressions;

namespace PixelCritic.WebApp.Repositories.Shared
{
    public interface IBaseRepo<TEntity, TKey> where TEntity : class
    {
        public Task addAsync(TEntity entity);
        public Task AddRangeAsync(IEnumerable<TEntity> entities);
        public Task<TEntity?> GetAsync(TKey id);
        public Task<TEntity?> ReadAsync(TKey id);
        public Task<TEntity?> ReadAsync(Expression<Func<TEntity, bool>> filter);
        public Task<List<TEntity>> ReadAllAsync();
        public Task<List<TEntity>> ReadAllAsync(Expression<Func<TEntity, bool>> filter);
        public Task<IEnumerable<TEntity>> ReadAllAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        public Task UpdateAsync(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpr);
        public Task DeleteAsync(Expression<Func<TEntity, bool>> filter);
    }
}
