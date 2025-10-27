namespace Dotnet.McpExample.ProductSeed;

public sealed class Product
{
    public string Name { get; }
    public string EanCode { get; }
    public decimal UnitNetPrice { get; }

    private Product(
        string name, 
        string eanCode,
        decimal unitNetPrice)
    {
        Name = name;
        EanCode = eanCode;
        UnitNetPrice = unitNetPrice;
    }

    public static Product Create(string name, string eanCode, decimal unitNetPrice)
        => new(
            name,
            eanCode,
            unitNetPrice);
}