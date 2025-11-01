using Dotnet.Mcp.Example.Core.Application.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

internal static class ApplicationConfigurationExtensions
{
    internal static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddScoped<IContractorsService, ContractorService>()
            .AddScoped<IOrdersService, OrdersService>()
            .AddScoped<IInvoicesService, InvoicesService>();
    }
}