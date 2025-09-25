using System.Linq.Expressions;

namespace Shared.Kernel.Data;

public interface IGenericRepository<TEntity> where TEntity : class
{
    // Get methods
    Task<TEntity> GetByIdAsync(object id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    // Add methods
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    // Update methods
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);

    // Remove methods
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);

    // Count
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

    // Any
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null);
}