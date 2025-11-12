using Dotnet.Mcp.Example.Core.Domain.Constants;
using Dotnet.Mcp.Example.Core.Domain.Entities;
using Dotnet.Mcp.Example.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Mcp.Example.Core.Application.Services;

public interface IOrdersService
{
    Task<Guid> CreateOrderAsync(
        Guid contractorId,
        CancellationToken cancellationToken);
    
    Task AddPositionAsync(
        Guid orderId,
        string productName,
        decimal unitPrice,
        decimal quantity,
        CancellationToken cancellationToken);
    
    Task DeleteOrderAsync(
        Guid orderId,
        CancellationToken cancellationToken);
}

internal sealed class OrdersService(
    ApplicationDbContext dbContext) : IOrdersService
{
    public async Task<Guid> CreateOrderAsync(Guid contractorId, CancellationToken cancellationToken)
    {
        var contractor = await dbContext
            .Set<Contractor>()
            .FirstOrDefaultAsync(c => c.Id == contractorId, cancellationToken);

        if (contractor is null)
        {
            throw new ArgumentException($"Contract with Id: {contractorId} not found");
        }
        
        var number = dbContext
            .Set<Order>()
            .Max(x => (int?)x.Number) ?? 0;

        var order = Order.Create(
            number + 1,
            DateTimeOffset.UtcNow,
            contractor);
        
        await dbContext
            .Set<Order>()
            .AddAsync(
                order,
                cancellationToken);
        
        await dbContext
            .SaveChangesAsync(cancellationToken);
        
        return order.Id;
    }

    public async Task AddPositionAsync(
        Guid orderId,
        string productName,
        decimal unitPrice,
        decimal quantity,
        CancellationToken cancellationToken)
    {
        var order = await dbContext
            .Set<Order>()
            .SingleOrDefaultAsync(x => x.Id == orderId, cancellationToken);

        if (order is null)
        {
            throw new ArgumentException("Order not found");
        }
        
        order.AddPosition(
            productName, 
            unitPrice,
            quantity);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await dbContext
            .Set<Order>()
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order is null)
        {
            throw new ArgumentException($"Order with Id: {orderId} not found");
        }

        if (order.Status == OrderStatus.Realized)
        {
            throw new InvalidOperationException("Cannot delete realized order");
        }

        dbContext.Set<Order>().Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}