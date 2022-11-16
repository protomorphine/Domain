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
    /// <summary>
    /// Сет сущностей
    /// </summary>
    private readonly DbSet<TEntity> _entities;

    /// <summary>
    /// Контекст бд
    /// </summary>
    private readonly TContext _dbContext;

    /// <summary>
    /// Создает новый экземпляр <see cref="BaseRepository{TEntity,T,TContext}"/>
    /// </summary>
    /// <param name="dbContext">Контекст бд</param>
    protected BaseRepository(TContext dbContext)
    {
        _entities = dbContext.Set<TEntity>();
        _dbContext = dbContext;
    }

    /// <summary>
    /// Метод создания сущности в бд
    /// </summary>
    /// <param name="entity">сущность</param>
    /// <returns>идентфиикатор сущности</returns>
    public virtual async Task<T> CreateAsync(TEntity entity)
    {
        await _entities.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    /// <summary>
    /// Метод обновления сущности в бд
    /// </summary>
    /// <param name="entity">сущность</param>
    public virtual async Task UpdateAsync(TEntity entity)
    {
        _entities.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Метод удаления сущности из бд
    /// </summary>
    /// <param name="entity">сущность</param>
    public virtual async Task DeleteAsync(TEntity entity)
    {
        _entities.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Метод поулчения сущноси из бд
    /// </summary>
    /// <param name="id">идентификатор сущности</param>
    /// <returns>сущность</returns>
    public virtual async Task<TEntity?> GetAsync(T id)
    {
        // ReSharper disable once HeapView.PossibleBoxingAllocation
        return await _entities.FirstOrDefaultAsync(it => it.Id!.Equals(id));
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }
}