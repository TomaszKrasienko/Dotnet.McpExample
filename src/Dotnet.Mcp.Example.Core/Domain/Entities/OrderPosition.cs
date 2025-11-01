namespace Dotnet.Mcp.Example.Core.Domain.Entities;

public sealed class OrderPosition
{
    public Guid Id { get; private set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; private set; }
    public decimal Quantity { get; private set; }

    private OrderPosition()
    {
        // EF Core constructor
    }

    private OrderPosition(
        string productName,
        decimal unitPrice,
        decimal quantity)
    {
        Id = Guid.NewGuid();
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    internal static OrderPosition Create(
        string productName,
        decimal unitPrice,
        decimal quantity)
        => new(productName, unitPrice, quantity);
}