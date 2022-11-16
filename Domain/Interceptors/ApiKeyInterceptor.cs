using Domain.Models;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Domain.Interceptors;

/// <summary>
/// Interceptor for authenticate requests
/// </summary>
public class ApiKeyInterceptor : Interceptor
{
    #region Fields

    /// <summary>
    /// Api key options
    /// </summary>
    private readonly ApiKeyOptions _apiKeyOptions;

    #endregion

    #region Constructors

    /// <summary>
    /// Create new instance of <see cref="ApiKeyInterceptor"/>
    /// </summary>
    /// <param name="apiKeyOptions">pai key options</param>
    public ApiKeyInterceptor(ApiKeyOptions apiKeyOptions) => _apiKeyOptions = apiKeyOptions;

    #endregion

    #region Methods

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var receivedApiKey = context.RequestHeaders.Get(_apiKeyOptions.Key)?.Value;

        if (string.IsNullOrEmpty(receivedApiKey))
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                $"No valid request security header received. Set '{_apiKeyOptions.Key}' header to authenticate request."));

        if (receivedApiKey == _apiKeyOptions.Value)
            return await continuation(request, context);

        throw new RpcException(new Status(StatusCode.Unauthenticated, "Invalid api key received."));
    }

    #endregion
}