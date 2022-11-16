namespace Domain.Core.Repositories;

/// <summary>
/// Base CRUD repository
/// </summary>
/// <typeparam name="TEntity">entity</typeparam>
/// <typeparam name="TId">entity id type</typeparam>
public interface IBaseRepository<TEntity, TId>
{
    /// <summary>
    /// Create new entry in database
    /// </summary>
    /// <param name="entity">entity</param>
    /// <returns>entity id</returns>
    Task<TId> CreateAsync(TEntity entity);

    /// <summary>
    /// Update entity in database
    /// </summary>
    /// <param name="entity">entity</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Delete entity from database
    /// </summary>
    /// <param name="entity">entity</param>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Get entity from database by id
    /// </summary>
    /// <param name="id">entity id</param>
    /// <returns>entity</returns>
    Task<TEntity?> GetAsync(TId id);

    /// <summary>
    /// Gets list of entities from database
    /// </summary>
    /// <returns>list of entities</returns>
    Task<List<TEntity>> GetAllAsync();
}