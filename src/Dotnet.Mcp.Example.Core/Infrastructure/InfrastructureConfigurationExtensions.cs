using Dotnet.Mcp.Example.Core.Application;
using Dotnet.Mcp.Example.Core.Application.Clients;
using Dotnet.Mcp.Example.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class InfrastructureConfigurationExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
        => services
            .AddDal()
            .AddSuppliers()
            .AddApplication();
    
    private static IServiceCollection AddDal(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql("Host=localhost;Port=5432;Database=mcpServer;Username=postgres;Password=postgres")
                .EnableSensitiveDataLogging());

        return services;
    }

    private static IServiceCollection AddSuppliers(this IServiceCollection services)
    {
        services.AddHttpClient<ISuppliersClient, SuppliersClient>();
        return services;
    }
}