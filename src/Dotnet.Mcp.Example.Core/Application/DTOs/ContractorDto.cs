namespace Dotnet.Mcp.Example.Core.Application.DTOs;

/// <summary>
/// Represents a contractor data transfer object containing contractor information and deletion status.
/// </summary>
public sealed class ContractorDto
{
    /// <summary>
    /// Gets or initializes the unique identifier of the contractor.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or initializes the name of the contractor.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes a value indicating whether the contractor has been deleted.
    /// </summary>
    public bool IsDeleted { get; init; }

    /// <summary>
    /// Gets or initializes the date and time when the contractor was deleted, if applicable.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; init; }
}
