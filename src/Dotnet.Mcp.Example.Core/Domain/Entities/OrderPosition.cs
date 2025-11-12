namespace Dotnet.Mcp.Example.Core.Domain.Entities;

public sealed class OrderPosition
{
    public Guid Id { get; }
    public string ProductName { get; }
    public decimal UnitPrice { get;  }
    public decimal Quantity { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private OrderPosition()
    {
        // EF Core constructor
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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