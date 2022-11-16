using Domain.Core.Entities;
using Domain.Core.Repositories;
using Domain.Infrastructure.Data;

namespace Domain.Infrastructure.Repositories;

public class ExampleEntityRepository : BaseRepository<ExampleEntity, long, AppDbContext>, IExampleEntityRepository
{
    public ExampleEntityRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}