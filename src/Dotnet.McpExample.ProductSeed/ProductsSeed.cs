namespace Dotnet.McpExample.ProductSeed;

public static class ProductsSeed
{
    private static readonly Random Random = new();

    private static readonly (string Name, string EanCode, decimal BasePrice)[] BaseProducts =
    [
        ("Laptop Dell Inspiron 15", "5901234567890", 2499.99m),
        ("USB-C Cable 2m", "5901234567891", 15.99m),
        ("Wireless Mouse Logitech", "5901234567892", 45.50m),
        ("Mechanical Keyboard RGB", "5901234567893", 129.99m),
        ("27\" Monitor Samsung", "5901234567894", 349.00m),
        ("Webcam HD 1080p", "5901234567895", 79.99m),
        ("External SSD 1TB", "5901234567896", 199.99m),
        ("HDMI Cable 3m", "5901234567897", 12.50m),
        ("Laptop Stand Aluminum", "5901234567898", 34.99m),
        ("Headphones Sony WH-1000XM4", "5901234567899", 299.00m),
        ("Phone Case iPhone 15", "5901234567900", 24.99m),
        ("Power Bank 20000mAh", "5901234567901", 49.99m),
        ("Smart Watch Garmin", "5901234567902", 399.99m),
        ("Bluetooth Speaker JBL", "5901234567903", 89.99m),
        ("Gaming Chair", "5901234567904", 249.00m),
        ("MacBook Pro 14\"", "5901234567905", 3299.00m),
        ("iPad Air 11\"", "5901234567906", 899.99m),
        ("AirPods Pro 2", "5901234567907", 249.99m),
        ("Magic Keyboard", "5901234567908", 149.00m),
        ("Magic Mouse 2", "5901234567909", 79.00m),
        ("Thunderbolt 4 Cable", "5901234567910", 29.99m),
        ("USB Hub 7-Port", "5901234567911", 39.99m),
        ("Ethernet Cable Cat6 5m", "5901234567912", 8.50m),
        ("Router TP-Link AX3000", "5901234567913", 129.00m),
        ("NAS Synology 2-Bay", "5901234567914", 549.99m),
        ("Graphics Card RTX 4070", "5901234567915", 1299.99m),
        ("RAM DDR5 32GB", "5901234567916", 189.99m),
        ("CPU AMD Ryzen 7", "5901234567917", 449.00m),
        ("Motherboard ASUS ROG", "5901234567918", 329.99m),
        ("PSU 850W Modular", "5901234567919", 159.99m),
        ("PC Case Mid Tower", "5901234567920", 99.99m),
        ("Cooling Fan RGB 120mm", "5901234567921", 19.99m),
        ("Thermal Paste Arctic", "5901234567922", 7.99m),
        ("Microphone Blue Yeti", "5901234567923", 119.00m),
        ("Ring Light LED", "5901234567924", 45.00m),
        ("Green Screen Portable", "5901234567925", 59.99m),
        ("Drawing Tablet Wacom", "5901234567926", 379.00m),
        ("Printer HP LaserJet", "5901234567927", 299.99m),
        ("Scanner Epson", "5901234567928", 189.00m),
        ("Docking Station USB-C", "5901234567929", 199.99m)
    ];

    public static IReadOnlyList<Product> GetProducts()
    {
        var products = new List<Product>();

        // Generate 50 products with slight price variations
        foreach (var baseProduct in BaseProducts)
        {
            var priceVariation = GeneratePriceVariation(baseProduct.BasePrice);

            products.Add(Product.Create(
                baseProduct.Name,
                baseProduct.EanCode,
                priceVariation));
        }

        return products.AsReadOnly();
    }

    private static decimal GeneratePriceVariation(decimal basePrice)
    {
        // Generate a small random variation (±2% of base price)
        var variationPercentage = (decimal)(Random.NextDouble() * 0.04 - 0.02); // -2% to +2%
        var variation = basePrice * variationPercentage;
        var newPrice = basePrice + variation;

        // Round to 2 decimal places
        return Math.Round(newPrice, 2);
    }
}
