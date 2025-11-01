using Dotnet.Mcp.Example.Core.Domain.Constants;

namespace Dotnet.Mcp.Example.Core.Domain.Entities;

public sealed class Order
{
    private readonly List<OrderPosition> _positions = [];
    public Guid Id { get; }
    public int Number { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public OrderStatus Status { get; private set; }
    public Guid ContractorId { get; private set; }
    public IReadOnlyCollection<OrderPosition> Positions => _positions.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Order()
    {
        // EF Core constructor
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    
    private Order(
        int number,
        DateTimeOffset createdAt,
        OrderStatus status,
        Guid contractorId)
    {
        Id = Guid.NewGuid();
        Number = number;
        CreatedAt = createdAt;
        Status = status;
        ContractorId = contractorId;
    }

    public static Order Create(
        int number,
        DateTimeOffset createdAt,
        Contractor contractor)
    {
        if (number <= 0)
        {
            throw new ArgumentException("Order number must be greater than zero.", nameof(number));
        }

        if (contractor is null)
        {
            throw new ArgumentNullException(nameof(contractor));
        }

        var order = new Order(
            number,
            createdAt,
            OrderStatus.Open,
            contractor.Id);
        
        return order;
    }

    public void AddPosition(
        string productName,
        decimal unitPrice,
        decimal quantity)
    {
        if (unitPrice <= 0)
        {
            throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPrice));
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }

        if (Status == OrderStatus.Confirmed)
        {
            throw new InvalidOperationException("Cannot add position to a closed order.");
        }

        var position = OrderPosition.Create(
            productName,
            unitPrice,
            quantity);
        
        _positions.Add(position);
    }

    public void RemovePosition(Guid positionId)
    {
        if (Status == OrderStatus.Confirmed)
        {
            throw new InvalidOperationException("Cannot remove position from a closed order.");
        }

        var position = Positions.FirstOrDefault(p => p.Id == positionId);
        if (position is null)
        {
            throw new InvalidOperationException($"Order position with ID '{positionId}' not found.");
        }

        _positions.Remove(position);
    }

    public void Confirm()
    {
        if (Status == OrderStatus.Confirmed)
        {
            throw new InvalidOperationException("Order is already Confirm.");
        }

        Status = OrderStatus.Confirmed;
    }

    public void MarkAsRealized()
    {
        if (Status != OrderStatus.Confirmed)
        {
            throw new InvalidOperationException("Cannot realize order with other status than confirm.");
        }

        Status = OrderStatus.Realized;
    }
}