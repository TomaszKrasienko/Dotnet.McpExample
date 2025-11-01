namespace Dotnet.Mcp.Example.Core.Domain.Constants;

public sealed record OrderStatus
{
    public static OrderStatus Open => new("Open");
    public static OrderStatus Confirmed => new("Confirmed");
    public static OrderStatus Realized => new("Realized");
    
    public string Value { get; }

    private OrderStatus(string value)
    {
        Value = value;
    }

    public static OrderStatus Parse(string value) => value switch
    {
        nameof(Open) => Open,
        nameof(Confirmed) => Confirmed,
        nameof(Realized) => Realized,
        _ => throw new ArgumentException($"Unknown order status value: {value}")
    };
}