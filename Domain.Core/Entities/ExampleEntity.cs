namespace Domain.Core.Entities;

/// <summary>
/// Example entity
/// </summary>
public class ExampleEntity : Entity<long>
{
    /// <summary>
    /// Example "Name" property
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Example "Content" property
    /// </summary>
    public string Content { get; set; } = string.Empty;
}