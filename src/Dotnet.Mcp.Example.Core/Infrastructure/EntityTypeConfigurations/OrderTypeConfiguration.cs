using Dotnet.Mcp.Example.Core.Domain.Constants;
using Dotnet.Mcp.Example.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet.Mcp.Example.Core.Infrastructure.EntityTypeConfigurations;

internal sealed class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
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
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasColumnName("Status")
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => OrderStatus.Parse(v));

        builder
            .Property(x => x.ContractorId)
            .HasColumnName("ContractorId")
            .IsRequired();

        builder.OwnsMany(x => x.Positions, position =>
        {
            position
                .ToTable("OrderPositions");
            
            position
                .WithOwner()
                .HasForeignKey("OrderId");

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
        });
    }
}
