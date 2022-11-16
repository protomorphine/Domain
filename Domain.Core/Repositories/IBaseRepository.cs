namespace Domain.Core.Repositories;

/// <summary>
/// Базовый репозиторий
/// </summary>
/// <typeparam name="TEntity">сущность</typeparam>
/// <typeparam name="TId">тип идентификатора сущности в бд</typeparam>
public interface IBaseRepository<TEntity, TId>
{
    /// <summary>
    /// Метод создания сущности в бд
    /// </summary>
    /// <param name="entity">сущность</param>
    /// <returns>идентфиикатор сущности</returns>
    Task<TId> CreateAsync(TEntity entity);

    /// <summary>
    /// Метод обновления сущности в бд
    /// </summary>
    /// <param name="entity">сущность</param>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Метод удаления сущности из бд
    /// </summary>
    /// <param name="entity">сущность</param>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Метод поулчения сущноси из бд
    /// </summary>
    /// <param name="id">идентификатор сущности</param>
    /// <returns>сущность</returns>
    Task<TEntity?> GetAsync(TId id);

    Task<List<TEntity>> GetAllAsync();
}