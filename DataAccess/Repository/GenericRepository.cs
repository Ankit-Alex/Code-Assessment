using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Zeiss_TakeHome.DataAccess.Data;

namespace DataAccess.Repository
{
    /// <summary>
    /// Generic Repository to abstract the Data Access Layer.
    /// </summary>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();    
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public IQueryable<T> GetAsQueryable()
        {
            return _dbSet.AsNoTracking().AsQueryable<T>();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
