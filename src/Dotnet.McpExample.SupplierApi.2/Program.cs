using Dotnet.McpExample.ProductSeed;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/products", () =>
    {
        var products = ProductsSeed.GetProducts();
        return Results.Ok(products);
    })
    .WithName("GetProducts");

app.Run();