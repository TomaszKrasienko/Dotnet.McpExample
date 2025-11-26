using System.ComponentModel;
using System.Text.Json;
using Dotnet.Mcp.Example.Core.Application.DTOs;
using Dotnet.Mcp.Example.Core.Domain.Entities;
using Dotnet.Mcp.Example.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace Dotnet.McpExample.App.Bootstrapper.Prompts;

[McpServerPromptType]
public sealed class InvoicesPrompts(ApplicationDbContext dbContext)
{
    [McpServerPrompt, Description("Gets all invoices with their details including contractor ID and positions")]
    public async Task<ChatMessage> GetInvoices(CancellationToken cancellationToken = default)
    {
        var invoices = await dbContext
            .Set<Invoice>()
            .Include(i => i.Positions)
            .ToListAsync(cancellationToken);

        if (!invoices.Any())
        {
            return new ChatMessage(
                ChatRole.User,
                "No invoices found in the system");
        }

        var invoiceDtos = invoices.Select(invoice => new InvoiceDto
        {
            Id = invoice.Id,
            Number = invoice.Number,
            CreatedAt = invoice.CreatedAt,
            ContractorId = invoice.ContractorId,
            Positions = invoice.Positions.Select(position => new InvoicePositionDto
            {
                Id = position.Id,
                UnitPrice = position.UnitPrice,
                Quantity = position.Quantity
            }).ToList()
        }).ToList();

        var json = JsonSerializer.Serialize(invoiceDtos, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        return new ChatMessage(
            ChatRole.User,
            json);
    }
}
