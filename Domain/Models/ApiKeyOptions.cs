namespace Domain.Models;

/// <summary>
/// Options to authenticate requests
/// </summary>
public class ApiKeyOptions
{
    /// <summary>
    /// Key name in headers
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Value in headers
    /// </summary>
    public string Value { get; set; } = string.Empty;
}