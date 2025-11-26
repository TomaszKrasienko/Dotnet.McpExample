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
public sealed class ContractorsPrompts(ApplicationDbContext dbContext)
{
    [McpServerPrompt, Description("Gets all contractors with their details")]
    public async Task<ChatMessage> GetAllContractors(CancellationToken cancellationToken = default)
    {
        var contractors = await dbContext
            .Set<Contractor>()
            .ToListAsync(cancellationToken);

        if (!contractors.Any())
        {
            return new ChatMessage(
                ChatRole.User,
                "No contractors found in the system");
        }

        var contractorDtos = contractors.Select(contractor => new ContractorDto
        {
            Id = contractor.Id,
            Name = contractor.Name,
            IsDeleted = contractor.IsDeleted,
            DeletedAt = contractor.DeletedAt
        }).ToList();

        var json = JsonSerializer.Serialize(contractorDtos, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        return new ChatMessage(
            ChatRole.User,
            json);
    }
}