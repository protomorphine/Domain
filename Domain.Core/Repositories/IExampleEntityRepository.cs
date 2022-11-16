using Domain.Core.Entities;

namespace Domain.Core.Repositories;

/// <summary>
/// Repository to work with <see cref="ExampleEntity"/>
/// </summary>
public interface IExampleEntityRepository : IBaseRepository<ExampleEntity, long>
{
    /// <summary>
    /// Get count of entities from database
    /// </summary>
    /// <returns>count of entities</returns>
    Task<long> GetCount();
}