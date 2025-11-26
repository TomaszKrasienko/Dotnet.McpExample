using System.ComponentModel;
using Dotnet.Mcp.Example.Core.Application.Services;
using ModelContextProtocol.Server;

namespace Dotnet.McpExample.App.Bootstrapper.Tools;

[McpServerToolType]
public sealed class InvoicesTool(IInvoicesService invoicesService)
{
    [McpServerTool, Description("Creates an invoice from an existing order and marks the order as realized")]
    public async Task<Guid> CreateFromOrderAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        var invoiceId = await invoicesService
            .CreateInvoiceFromOrderAsync(
                orderId,
                cancellationToken);
        
        return invoiceId;
    }
}