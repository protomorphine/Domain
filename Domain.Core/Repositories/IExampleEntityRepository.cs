using Domain.Core.Entities;

namespace Domain.Core.Repositories;

/// <summary>
/// Repository to work with <see cref="ExampleEntity"/>
/// </summary>
public interface IExampleEntityRepository : IBaseRepository<ExampleEntity, long>
{
}