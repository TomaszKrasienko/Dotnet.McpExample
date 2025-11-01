
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class Configuration
{
    public static IServiceCollection AddCore(this IServiceCollection service)
        => service
            .AddApplication()
            .AddInfrastructure();
}