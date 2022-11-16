using System.Reflection;
using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Infrastructure.Data;

public class AppDbContext : DbContext
{
    /// <summary>
    /// Строка подключения к базе данных
    /// </summary>
    private readonly string? _connectionString;

    /// <summary>
    /// Коллекция сущностей - пользователь
    /// </summary>
    public DbSet<ExampleEntity> Entities { get; set; }

    /// <summary>
    /// Создает новый экземпляр <see cref="AppDbContext"/>
    /// </summary>
    /// <param name="options"><see cref="DbContextOptions"/></param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Создает новый экземпляр <see cref="ApplicationDbContext"/>
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных</param>
    public AppDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}