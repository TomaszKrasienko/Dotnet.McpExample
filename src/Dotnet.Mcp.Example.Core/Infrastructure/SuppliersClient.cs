using System.Text.Json;
using Dotnet.Mcp.Example.Core.Application.Clients;

namespace Dotnet.Mcp.Example.Core.Infrastructure;

/// <inheritdoc cref="ISuppliersClient"/>
internal sealed class SuppliersClient(HttpClient httpClient) : ISuppliersClient
{
    public string TechSolutionName => "Tech Solutions Ltd.";
    public string GlobalInnovationsName => "Global Innovations Inc.";
    
    public async Task<string> GetTechSolutionProductsAsync()
    {
        try
        {
            var response = await httpClient.GetStringAsync("http://localhost:5002/products");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var formattedData = JsonSerializer.Serialize(
                JsonSerializer.Deserialize<JsonElement>(response),
                options);

            return formattedData;
        }
        catch (Exception ex)
        {
            return $"There was an error processing the products with exception {ex.Message}";
        }
    }

    public async Task<string> GetGlobalInnovationsProductsAsync()
    {
        try
        {
            var response = await httpClient.GetStringAsync("http://localhost:5001/products");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var formattedData = JsonSerializer.Serialize(
                JsonSerializer.Deserialize<JsonElement>(response),
                options);

            return formattedData;
        }
        catch (Exception ex)
        {
            return $"There was an error processing the products with exception {ex.Message}";
        }
    }
}