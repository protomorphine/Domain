using Domain.Core.Repositories;
using Domain.Infrastructure.Repositories;

namespace Domain.Extensions;

public static class DependencyExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IExampleEntityRepository, ExampleEntityRepository>();
    }
}