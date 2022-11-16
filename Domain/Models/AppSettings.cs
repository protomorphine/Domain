namespace Domain.Models;

/// <summary>
/// Applications settings
/// </summary>
public class AppSettings
{
    /// <inheritdoc cref="DbOptions"/>
    public DbOptions DbOptions { get; set; } = new();

    /// <inheritdoc cref="ApiKeyOptions"/>
    public ApiKeyOptions ApiKeyOptions { get; set; } = new();
}