using Dotnet.Mcp.Example.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Mcp.Example.Core.Infrastructure.EntityTypeConfigurations;

internal sealed class InvoiceTypeConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("Id");

        builder
            .Property(x => x.Number)
            .HasColumnName("Number")
            .IsRequired();
        
        builder
            .Property(x => x.RelatedOrder)
            .HasColumnName("RelatedOrder")
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();

        builder
            .Property(x => x.ContractorId)
            .HasColumnName("ContractorId")
            .IsRequired();

        builder.OwnsMany(x => x.Positions, position =>
        {
            position
                .WithOwner()
                .HasForeignKey("PositionId");
            
            position
                .HasKey(x => x.Id);
            
            position
                .Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired()
                .HasColumnName("Id");

            position
                .Property(x => x.UnitPrice)
                .IsRequired()
                .HasColumnName("UnitPrice")
                .HasPrecision(18, 2);

            position.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasPrecision(18, 2);

            position
                .Property(x => x.ProductName)
                .HasColumnName("ProductName")
                .IsRequired();
        });
    }
}