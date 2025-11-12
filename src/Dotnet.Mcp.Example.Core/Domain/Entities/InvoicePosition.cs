namespace Dotnet.Mcp.Example.Core.Domain.Entities;

public sealed class InvoicePosition
{
    public Guid Id { get; }
    public decimal UnitPrice { get; }
    public decimal Quantity { get; }
    public string ProductName { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private InvoicePosition()
    {
        // EF Core constructor
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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