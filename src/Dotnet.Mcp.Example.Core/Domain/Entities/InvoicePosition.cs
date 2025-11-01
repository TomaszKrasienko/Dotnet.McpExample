namespace Dotnet.Mcp.Example.Core.Domain.Entities;

public sealed class InvoicePosition
{
    public Guid Id { get; }
    public decimal UnitPrice { get; }
    public decimal Quantity { get; }
    public string ProductName { get; }

    private InvoicePosition()
    {
        // EF Core constructor
    }

    private InvoicePosition(
        decimal unitPrice,
        decimal quantity,
        string productName)
    {
        Id = Guid.NewGuid();
        UnitPrice = unitPrice;
        Quantity = quantity;
        ProductName = productName;
    }

    internal static InvoicePosition Create(
        decimal unitPrice,
        decimal quantity,
        string productName)
        => new (
            unitPrice,
            quantity,
            productName);
}