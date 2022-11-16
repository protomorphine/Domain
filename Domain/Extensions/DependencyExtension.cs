using Domain.Core.Repositories;
using Domain.Core.Services;
using Domain.Infrastructure.Repositories;

namespace Domain.Extensions;

/// <summary>
/// Dependency extensions
/// </summary>
public static class DependencyExtension
{
    /// <summary>
    /// Register repositories in DI container
    /// </summary>
    /// <param name="services">service collection</param>
    public static void AddRepositories(this IServiceCollection services)
        => services.AddTransient<IExampleEntityRepository, ExampleEntityRepository>();

    /// <summary>
    /// Add gRPC services in application pipeline
    /// </summary>
    /// <param name="app">application</param>
    public static void AddGrpcServices(this WebApplication app)
        => app.MapGrpcService<ExampleService>();
}