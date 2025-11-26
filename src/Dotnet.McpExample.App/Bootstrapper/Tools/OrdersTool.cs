using System.ComponentModel;
using Dotnet.Mcp.Example.Core.Application.Services;
using ModelContextProtocol.Server;

namespace Dotnet.McpExample.App.Bootstrapper.Tools;

[McpServerToolType]
public sealed class OrdersTool(IOrdersService service)
{
    [McpServerTool, Description("Creates a new order for a contractor")]
    public async Task<Guid> CreateOrderAsync(
        Guid contractorId,
        CancellationToken cancellationToken = default)
    {
        var orderId = await service.CreateOrderAsync(contractorId, cancellationToken);
        return orderId;
    }

    [McpServerTool, Description("Deletes an order")]
    public async Task DeleteOrderAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        await service.DeleteOrderAsync(
            orderId,
            cancellationToken);
    }
    
    [McpServerTool, Description("Confirms an order")]
    public async Task ConfirmOrderAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        await service.ConfirmOrderAsync(
            orderId,
            cancellationToken);
    }

    [McpServerTool, Description("Adds a position to an existing order")]
    public async Task AddOrderPositionAsync(
        Guid orderId,
        string productName,
        decimal unitPrice,
        decimal quantity,
        CancellationToken cancellationToken = default)
    {
        await service.AddPositionAsync(
            orderId, productName, unitPrice, quantity, cancellationToken);
    }
}