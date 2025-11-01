namespace Dotnet.Mcp.Example.Core.Domain.Entities;

public sealed class Invoice
{
    private readonly List<InvoicePosition> _positions = [];
    public Guid Id { get; }
    public Guid RelatedOrder { get; }
    public int Number { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid ContractorId { get; private set; }
    public IReadOnlyCollection<InvoicePosition> Positions => _positions.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Invoice()
    {
        // EF Core constructor
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Invoice(
        int number,
        Guid relatedOrder,
        DateTimeOffset createdAt,
        Guid contractorId,
        List<InvoicePosition> positions)
    {
        Id = Guid.NewGuid();
        RelatedOrder = relatedOrder;
        Number = number;
        CreatedAt = createdAt;
        ContractorId = contractorId;
        _positions = positions;
    }

    public static Invoice Create(
        int number,
        Order order,
        DateTimeOffset createdAt)
    {
        List<InvoicePosition> invoicePositions = [];
        
        foreach (var position in order.Positions)
        {
            var invoicePosition = InvoicePosition.Create(
                position.UnitPrice,
                position.Quantity,
                position.ProductName);
            
            invoicePositions.Add(invoicePosition);
        }

        var invoice = new Invoice(
            number,
            order.Id,
            createdAt,
            order.ContractorId,
            invoicePositions);
        
        return invoice;
    }
}