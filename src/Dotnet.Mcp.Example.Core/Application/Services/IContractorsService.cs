using Dotnet.Mcp.Example.Core.Domain.Entities;
using Dotnet.Mcp.Example.Core.Infrastructure;

namespace Dotnet.Mcp.Example.Core.Application.Services;

public interface IContractorsService
{
    Task<Contractor> CreateAsync(string name, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}

internal sealed class ContractorService(ApplicationDbContext context) : IContractorsService
{
    public async Task<Contractor> CreateAsync(string name, CancellationToken cancellationToken)
    {
        var contractor = Contractor.Create(name);

        await context.Set<Contractor>().AddAsync(contractor, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return contractor;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var contractor = await context
            .Set<Contractor>()
            .FindAsync(keyValues: [id], cancellationToken: cancellationToken);

        if (contractor is null)
        {
            throw new ArgumentException("Contract or not found");
        }
        
        contractor.Delete();
        
        await context.SaveChangesAsync(cancellationToken);
    }
}