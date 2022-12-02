using Domain.Core.Exceptions;

namespace Domain.Core.Extensions;

/// <summary>
/// Extensions of ObjectNotFoundException
/// </summary>
public static class ObjectNotFoundExtensions
{
    /// <summary>
    /// Throw exception when object equals null
    /// </summary>
    /// <param name="obj">object</param>
    /// <param name="message">exception message</param>
    /// <typeparam name="T">object type</typeparam>
    /// <exception cref="ObjectNotFoundException">exception</exception>
    public static void ThrowObjectNotFoundIfNull<T>(this T obj, string message)
    where T : class?
    {
        if (obj == null) throw new ObjectNotFoundException(message);
    }
}