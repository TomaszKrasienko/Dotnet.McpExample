using System.ComponentModel;
using Dotnet.Mcp.Example.Core.Application.Services;
using ModelContextProtocol.Server;

namespace Dotnet.McpExample.App.Bootstrapper.Tools;

[McpServerToolType]
public sealed class ContractorsTool(
    IContractorsService contractorsService)
{
    [McpServerTool, Description("Creates a new contractor with the specified name")]
    public async Task<string> CreateContractorAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        var contractor = await contractorsService.CreateAsync(
            name,
            cancellationToken);
        
        return contractor.Id.ToString();
    }

    [McpServerTool, Description("Soft deletes a contractor by marking it as deleted")]
    public async Task DeleteContractorAsync(
        Guid contractorId,
        CancellationToken cancellationToken = default)
    {
        await contractorsService.DeleteAsync(contractorId, cancellationToken);
    }
}