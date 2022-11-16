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

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    #endregion

}