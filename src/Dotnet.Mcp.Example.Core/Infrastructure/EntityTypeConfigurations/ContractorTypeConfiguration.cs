using Dotnet.Mcp.Example.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Mcp.Example.Core.Infrastructure.EntityTypeConfigurations;

internal sealed class ContractorTypeConfiguration : IEntityTypeConfiguration<Contractor>
{
    public void Configure(EntityTypeBuilder<Contractor> builder)
    {
        builder
            .HasKey(c => c.Id);
        
        builder
            .Property(x => x.Id)
            .IsRequired()
            .HasColumnName("Id");
        
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name");
        
        builder
            .Property(x => x.IsDeleted)
            .IsRequired()
            .HasColumnName("IsDeleted");
        
        builder
            .Property(x => x.DeletedAt)
            .HasColumnName("DeletedAt");

        builder
            .HasQueryFilter(c => !c.IsDeleted);
    }
}