

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq.Expressions;

namespace PixelCritic.WebApp.Repositories.Shared
{
    //Källa https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    public class BaseRepo<TEntity, TKey> : IBaseRepo<TEntity, TKey> where TEntity : class
    {
        private readonly PixelDbContext _context;
        public BaseRepo(PixelDbContext context)
        {
            _context = context;
        }

        #region Create
        public async Task addAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);
        

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
            => await _context.AddRangeAsync(entities);
        #endregion

        #region Read
        public async Task<TEntity?> GetAsync(TKey id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity?> ReadAsync(TKey id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity is null) return null!;
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
        
        public async Task<TEntity?> ReadAsync(Expression<Func<TEntity, bool>> filter)
        {
            var entity = await _context.Set<TEntity>().Where(filter).FirstOrDefaultAsync();
            if (entity is null) return null!;
            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<List<TEntity>> ReadAllAsync()
            => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<List<TEntity>> ReadAllAsync(Expression<Func<TEntity, bool>> filter)
            => await _context.Set<TEntity>().AsNoTracking().Where(filter).ToListAsync();

        /// <summary>
        /// <see href="https://antondevtips.com/blog/understanding-change-tracking-for-better-performance-in-ef-core"/>
       
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
       
        public async Task<IEnumerable<TEntity>> ReadAllAsync(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
            => await orderBy(_context.Set<TEntity>().AsNoTracking().Where(filter)).ToListAsync();
        #endregion
        

        #region Update
        /// <summary>
        
        /// <see href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.relationalqueryableextensions.executeupdateasync?view=efcore-8.0"/>
      
        public async Task UpdateAsync(
            Expression<Func<TEntity, bool>> filter, 
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpr)
            => await _context.Set<TEntity>().Where(filter).ExecuteUpdateAsync(updateExpr);
        #endregion

        #region Delete
        /// <summary>
      
        /// <see href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.relationalqueryableextensions.executedeleteasync?view=efcore-8.0"/>
   
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter)
            => await _context.Set<TEntity>().Where(filter).ExecuteDeleteAsync();
        #endregion
    }
}
