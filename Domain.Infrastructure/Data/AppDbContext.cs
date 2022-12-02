using System.Reflection;
using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.Data;

public class AppDbContext : DbContext
{
    #region Fields

    /// <summary>
    /// Database connection string
    /// </summary>
    private readonly string? _connectionString;

    /// <summary>
    /// Set of entities
    /// </summary>
    public DbSet<ExampleEntity>? Entities { get; set; }

    #endregion

    #region Contructors

    /// <summary>
    /// Create new instance of <see cref="AppDbContext"/>
    /// </summary>
    /// <param name="options"><see cref="DbContextOptions"/></param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>
    /// Create new instance of <see cref="AppDbContext"/>
    /// </summary>
    /// <param name="connectionString">database connection string</param>
    public AppDbContext(string connectionString) => _connectionString = connectionString;

    #endregion

    #region Methods

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <inheritdoc />
    public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        AddCreatedUpdatedAt();
        return await base.SaveChangesAsync(ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Add CreatedAt and UpdatedAt to entries
    /// </summary>
    private void AddCreatedUpdatedAt()
    {
        var entries = ChangeTracker.Entries();
        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property("CreatedAt").CurrentValue = now;
                    entry.Property("UpdatedAt").CurrentValue = now;
                    break;
                case EntityState.Modified:
                    entry.Property("UpdatedAt").CurrentValue = now;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    continue;
            }
        }
    }

    #endregion

}