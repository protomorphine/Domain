using Domain.Core.Entities;
using Domain.Core.Repositories;
using Domain.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.Repositories;

/// <inheritdoc />
public class BaseRepository<TEntity, T, TContext> : IBaseRepository<TEntity, T>
    where TEntity : class, IEntity<T>
    where TContext : AppDbContext
{
    #region Fields

    /// <summary>
    /// Entity collection
    /// </summary>
    private readonly DbSet<TEntity> _entities;

    /// <summary>
    /// Database context
    /// </summary>
    private readonly TContext _dbContext;

    #endregion

    #region Constructors

    /// <summary>
    /// Create new instance of <see cref="BaseRepository{TEntity,T,TContext}"/>
    /// </summary>
    /// <param name="dbContext">database context</param>
    protected BaseRepository(TContext dbContext)
    {
        _entities = dbContext.Set<TEntity>();
        _dbContext = dbContext;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync(TEntity entity)
    {
        await _entities.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    /// <inheritdoc />
    public virtual async Task UpdateAsync(TEntity entity)
    {
        _entities.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(TEntity entity)
    {
        _entities.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public virtual async Task<TEntity?> GetAsync(T id)
        => await _entities.AsNoTracking().FirstOrDefaultAsync(it => it.Id!.Equals(id));

    /// <inheritdoc />
    public async Task<List<TEntity>> GetAllAsync()
        => await _entities.AsNoTracking().ToListAsync();

    #endregion
}