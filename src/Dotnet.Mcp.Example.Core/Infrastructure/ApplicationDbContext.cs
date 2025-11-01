using Dotnet.Mcp.Example.Core.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Mcp.Example.Core.Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ContractorTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceTypeConfiguration());
    }
}