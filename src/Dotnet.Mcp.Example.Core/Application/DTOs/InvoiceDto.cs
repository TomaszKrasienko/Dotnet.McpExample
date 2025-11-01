namespace Dotnet.Mcp.Example.Core.Application.DTOs;

/// <summary>
/// Represents an invoice data transfer object containing invoice details and positions.
/// </summary>
public sealed class InvoiceDto
{
    /// <summary>
    /// Gets or initializes the unique identifier of the invoice.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or initializes the invoice number.
    /// </summary>
    public int Number { get; init; }

    /// <summary>
    /// Gets or initializes the date and time when the invoice was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Gets or initializes the unique identifier of the contractor associated with this invoice.
    /// </summary>
    public Guid ContractorId { get; init; }

    /// <summary>
    /// Gets or initializes the collection of invoice positions.
    /// </summary>
    public List<InvoicePositionDto> Positions { get; init; } = [];
}

/// <summary>
/// Represents a single position (line item) within an invoice.
/// </summary>
public sealed class InvoicePositionDto
{
    /// <summary>
    /// Gets or initializes the unique identifier of the invoice position.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or initializes the unit price of the item.
    /// </summary>
    public decimal UnitPrice { get; init; }

    /// <summary>
    /// Gets or initializes the quantity of the item invoiced.
    /// </summary>
    public decimal Quantity { get; init; }
}
