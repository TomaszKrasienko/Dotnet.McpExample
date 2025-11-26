using System.Text.Json;
using Dotnet.Mcp.Example.Core.Application.DTOs;
using Dotnet.Mcp.Example.Core.Domain.Entities;
using Dotnet.Mcp.Example.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;

namespace Dotnet.McpExample.App.Bootstrapper.Resources;

[McpServerResourceType]
public sealed class ContractorResources(ApplicationDbContext dbContext)
{
    [McpServerResource(UriTemplate = "contractors://list",
        Name = "Get all contractors resource",
        MimeType = "application/json")]
    public async Task<string> GetAllContractorsResource(CancellationToken cancellationToken)
    {
        var contractors = await dbContext
            .Set<Contractor>()
            .ToListAsync(cancellationToken);
        
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

        return json;
    }
}