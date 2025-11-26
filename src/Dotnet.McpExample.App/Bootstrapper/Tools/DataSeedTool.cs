using System.ComponentModel;
using Dotnet.Mcp.Example.Core.Domain.Entities;
using Dotnet.Mcp.Example.Core.Infrastructure;
using Dotnet.McpExample.ProductSeed;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.Server;

namespace Dotnet.McpExample.App.Bootstrapper.Tools;

[McpServerToolType]
public sealed class DataSeedTool(ApplicationDbContext dbContext)
{
    private static readonly Random Random = new();

    [McpServerTool, Description("Seeds the database with 3 contractors and 60 realized orders with invoices")]
    public async Task<string> SeedDataAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var existingContractorsCount = await dbContext
                .Set<Contractor>()
                .CountAsync(cancellationToken);
            
            if (existingContractorsCount > 0)
            {
                return "Database already contains data. Clear data first before seeding.";
            }

            var contractors = new List<Contractor>
            {
                Contractor.Create("Tech Solutions Ltd."),
                Contractor.Create("Global Innovations Inc.")
            };

            await dbContext
                .Set<Contractor>()
                .AddRangeAsync(contractors, cancellationToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);

            var products = ProductsSeed.GetProducts();

            var orders = new List<Order>();
            var invoices = new List<Invoice>();
            var currentDate = DateTimeOffset.UtcNow.AddMonths(-6); 

            for (int i = 1; i <= 60; i++)
            {
                var contractor = contractors[Random.Next(contractors.Count)];

                var order = Order.Create(
                    number: i,
                    createdAt: currentDate.AddDays(i * 3),
                    contractor: contractor);

                var positionCount = Random.Next(2, 9);
                for (int j = 0; j < positionCount; j++)
                {
                    var product = products[Random.Next(products.Count)];
                    var quantity = Random.Next(1, 11);
                    order.AddPosition(
                        product.Name,
                        product.UnitNetPrice, 
                        quantity);
                }

                order.Confirm();
                order.MarkAsRealized();

                orders.Add(order);

                var invoice = Invoice.Create(
                    number: i,
                    order,
                    createdAt: currentDate.AddDays(i * 3 + 1));
                
                invoices.Add(invoice);
            }

            await dbContext
                .Set<Order>()
                .AddRangeAsync(orders, cancellationToken);
            
            await dbContext
                .Set<Invoice>()
                .AddRangeAsync(invoices, cancellationToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);

            return $"Successfully seeded database with {contractors.Count} contractors, {orders.Count} orders, and {invoices.Count} invoices.";
        }
        catch (Exception ex)
        {
            return $"Failed to seed data: {ex.Message}";
        }
    }

    [McpServerTool, Description("Clears all data from the database (contractors, orders, and invoices)")]
    public async Task<string> ClearDataAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var invoices = await dbContext
                .Set<Invoice>()
                .ToListAsync(cancellationToken);
            
            dbContext
                .Set<Invoice>()
                .RemoveRange(invoices);

            var orders = await dbContext
                .Set<Order>()
                .ToListAsync(cancellationToken);
            
            dbContext
                .Set<Order>()
                .RemoveRange(orders);

            var contractors = await dbContext
                .Set<Contractor>()
                .IgnoreQueryFilters()
                .ToListAsync(cancellationToken);
            
            dbContext.Set<Contractor>().RemoveRange(contractors);

            await dbContext.SaveChangesAsync(cancellationToken);

            return $"Successfully cleared all data from the database. Removed {contractors.Count} contractors, {orders.Count} orders, and {invoices.Count} invoices.";
        }
        catch (Exception ex)
        {
            return $"Failed to clear data: {ex.Message}";
        }
    }
}
