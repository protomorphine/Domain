using Domain.Core.Entities;
using Domain.Core.Repositories;
using Domain.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.Repositories;

/// <inheritdoc cref="Domain.Core.Repositories.IExampleEntityRepository" />
public class ExampleEntityRepository : BaseRepository<ExampleEntity, long, AppDbContext>, IExampleEntityRepository
{
    #region Fields

    private readonly AppDbContext _dbContext;

    private readonly DbSet<ExampleEntity> _exampleEntities;

    #endregion
    
    #region Constrictors

    /// <summary>
    /// Create new instance of <see cref="ExampleEntityRepository"/>
    /// </summary>
    /// <param name="dbContext">database context</param>
    public ExampleEntityRepository(AppDbContext dbContext) : base(dbContext) {
        _dbContext = dbContext;
        _exampleEntities = dbContext.Entities!;
    }

    #endregion

    public async Task<long> GetCount() => await _exampleEntities.CountAsync();
}