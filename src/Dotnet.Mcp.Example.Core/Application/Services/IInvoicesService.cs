using Dotnet.Mcp.Example.Core.Domain.Constants;
using Dotnet.Mcp.Example.Core.Domain.Entities;
using Dotnet.Mcp.Example.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Mcp.Example.Core.Application.Services;

public interface IInvoicesService
{
    Task<Guid> CreateInvoiceFromOrderAsync(
        Guid orderId,
        CancellationToken cancellationToken);
}

internal sealed class InvoicesService(
    ApplicationDbContext dbContext) : IInvoicesService
{
    public async Task<Guid> CreateInvoiceFromOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await dbContext
            .Set<Order>()
            .Include(x => x.Positions)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order is null)
        {
            throw new ArgumentException($"Order with Id: {orderId} not found");
        }

        var number = dbContext
            .Set<Invoice>()
            .Max(x => (int?)x.Number) ?? 0;

        var invoice = Invoice.Create(
            number + 1,
            order,
            DateTimeOffset.UtcNow);

        await dbContext
            .Set<Invoice>()
            .AddAsync(
                invoice, 
                cancellationToken);
        
        order.MarkAsRealized();
        
        await dbContext
            .SaveChangesAsync(cancellationToken);

        return invoice.Id;
    }
}
