namespace Dotnet.Mcp.Example.Core.Application.Clients;

/// <summary>
/// Defines a client for interacting with supplier systems to retrieve product information.
/// </summary>
public interface ISuppliersClient
{
    /// <summary>
    /// Gets the name of the Tech Solution supplier.
    /// </summary>
    public string TechSolutionName { get; }

    /// <summary>
    /// Gets the name of the Global Innovations supplier.
    /// </summary>
    public string GlobalInnovationsName { get; }

    /// <summary>
    /// Retrieves the product catalog from the Tech Solution supplier asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing the products data as a string.</returns>
    Task<string> GetTechSolutionProductsAsync();

    /// <summary>
    /// Retrieves the product catalog from the Global Innovations supplier asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing the products data as a string.</returns>
    Task<string> GetGlobalInnovationsProductsAsync();
}