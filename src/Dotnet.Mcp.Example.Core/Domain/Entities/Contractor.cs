namespace Dotnet.Mcp.Example.Core.Domain.Entities;

public sealed class Contractor
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Contractor()
    {
        // EF Core constructor
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Contractor(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsDeleted = false;
        DeletedAt = null;
    }

    public static Contractor Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Contractor name cannot be empty or whitespace.", nameof(name));
        }

        return new(name);
    }

    public void Delete()
    {
        if (IsDeleted)
        {
            return;
        }

        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
    }
}