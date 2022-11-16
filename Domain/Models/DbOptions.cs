namespace Domain.Models;

/// <summary>
/// Database connection options
/// </summary>
public class DbOptions
{
    /// <summary>
    /// Database connection string
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
}