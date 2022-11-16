using Domain.Core.Entities;
using Domain.Core.Repositories;
using Domain.Infrastructure.Data;

namespace Domain.Infrastructure.Repositories;

/// <inheritdoc cref="Domain.Core.Repositories.IExampleEntityRepository" />
public class ExampleEntityRepository : BaseRepository<ExampleEntity, long, AppDbContext>, IExampleEntityRepository
{
    #region Constrictors

    /// <summary>
    /// Create new instance of <see cref="ExampleEntityRepository"/>
    /// </summary>
    /// <param name="dbContext">database context</param>
    public ExampleEntityRepository(AppDbContext dbContext) : base(dbContext) { }

    #endregion

}