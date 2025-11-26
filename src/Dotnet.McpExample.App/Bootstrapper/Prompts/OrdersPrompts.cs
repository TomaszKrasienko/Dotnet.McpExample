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
public sealed class OrdersPrompts(ApplicationDbContext dbContext)
{
    [McpServerPrompt, Description("Gets counted orders grouped by month and contractor")]
    public async Task<ChatMessage> GetCountedMonthlyOrders(CancellationToken cancellationToken = default)
    {
        var orders = await dbContext
            .Set<Order>()
            .Include(o => o.Positions)
            .ToListAsync(cancellationToken);

        if (!orders.Any())
        {
            return new ChatMessage(
                ChatRole.User,
                "No orders found in the system");
        }

        var orderDtos = orders.Select(order => new OrderDto
        {
            Id = order.Id,
            Number = order.Number,
            CreatedAt = order.CreatedAt,
            Status = order.Status.Value,
            ContractorId = order.ContractorId,
            Positions = order.Positions.Select(position => new OrderPositionDto
            {
                Id = position.Id,
                UnitPrice = position.UnitPrice,
                Quantity = position.Quantity
            }).ToList()
        }).ToList();

        var json = JsonSerializer.Serialize(orderDtos, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        return new ChatMessage(
            ChatRole.User,
            $"Using provided data get counted orders grouped by month and contractor. Data: {json}");
    }
    
    [McpServerPrompt, Description("Gets all orders with their details including contractor ID, status, and positions")]
    public async Task<ChatMessage> GetOrders(CancellationToken cancellationToken = default)
    {
        var orders = await dbContext
            .Set<Order>()
            .Include(o => o.Positions)
            .ToListAsync(cancellationToken);

        if (!orders.Any())
        {
            return new ChatMessage(
                ChatRole.User,
                "No orders found in the system");
        }

        var orderDtos = orders.Select(order => new OrderDto
        {
            Id = order.Id,
            Number = order.Number,
            CreatedAt = order.CreatedAt,
            Status = order.Status.Value,
            ContractorId = order.ContractorId,
            Positions = order.Positions.Select(position => new OrderPositionDto
            {
                Id = position.Id,
                UnitPrice = position.UnitPrice,
                Quantity = position.Quantity
            }).ToList()
        }).ToList();

        var json = JsonSerializer.Serialize(orderDtos, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        return new ChatMessage(
            ChatRole.User,
            json);
    }
}
