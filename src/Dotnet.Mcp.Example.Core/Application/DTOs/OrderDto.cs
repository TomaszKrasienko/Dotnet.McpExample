namespace Dotnet.Mcp.Example.Core.Application.DTOs;

/// <summary>
/// Represents an order data transfer object containing order details and positions.
/// </summary>
public sealed class OrderDto
{
    /// <summary>
    /// Gets or initializes the unique identifier of the order.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or initializes the order number.
    /// </summary>
    public int Number { get; init; }

    /// <summary>
    /// Gets or initializes the date and time when the order was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets or initializes the current status of the order.
    /// </summary>
    public string Status { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes the unique identifier of the contractor associated with this order.
    /// </summary>
    public Guid ContractorId { get; init; }

    /// <summary>
    /// Gets or initializes the collection of order positions.
    /// </summary>
    public List<OrderPositionDto> Positions { get; init; } = [];
}

/// <summary>
/// Represents a single position (line item) within an order.
/// </summary>
public sealed class OrderPositionDto
{
    /// <summary>
    /// Gets or initializes the unique identifier of the order position.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or initializes the unit price of the item.
    /// </summary>
    public decimal UnitPrice { get; init; }

    /// <summary>
    /// Gets or initializes the quantity of the item ordered.
    /// </summary>
    public decimal Quantity { get; init; }
}
