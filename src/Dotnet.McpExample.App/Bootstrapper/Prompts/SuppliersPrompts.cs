using System.ComponentModel;
using Dotnet.Mcp.Example.Core.Application.Clients;
using Dotnet.Mcp.Example.Core.Domain.Entities;
using Dotnet.Mcp.Example.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace Dotnet.McpExample.App.Bootstrapper.Prompts;

[McpServerPromptType]
public sealed class SuppliersPrompts(
    ISuppliersClient suppliersClient,
    ApplicationDbContext dbContext)
{
    [McpServerPrompt, Description("Gets supplier products - possible arguments: Tech Solutions Ltd., Global Innovations Inc.")]
    public async Task<ChatMessage> GetSuppliersProducts(string supplierName)
    {
        string data = string.Empty;
        
        var supplier = await dbContext
            .Set<Contractor>()
            .SingleOrDefaultAsync(x => x.Name == supplierName);

        if (supplier is null)
        {
            return new ChatMessage(
                ChatRole.User,
                "Contractor was not found");
        }
        
        if (supplier.Name == suppliersClient.TechSolutionName)
        {
            data = await suppliersClient.GetTechSolutionProductsAsync();
        }
        else if  (supplier.Name == suppliersClient.GlobalInnovationsName)
        {
            data = await suppliersClient.GetGlobalInnovationsProductsAsync();
        }
        else
        {
            return new ChatMessage(
                ChatRole.User,
                "Provided invalid supplier name");
        }
            
        return new ChatMessage(
            ChatRole.User,
            $"Return supplier with name: {supplierName} in table data from: {data}");
    }
}