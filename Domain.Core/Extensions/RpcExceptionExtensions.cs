using Grpc.Core;

namespace Domain.Core.Extensions;

/// <summary>
/// Extensions of gRPC exceptions
/// </summary>
public static class RpcExceptionExtensions
{
    /// <summary>
    /// Throw exception when object equals null
    /// </summary>
    /// <param name="obj">object</param>
    /// <param name="message">exception message</param>
    /// <typeparam name="T">object type</typeparam>
    /// <exception cref="RpcException">exception</exception>
    public static void ThrowRpcEntityNotFoundIfNull<T>(this T obj, string message)
    {
        if (obj == null) throw new RpcException(new Status(StatusCode.NotFound, message));
    }
}