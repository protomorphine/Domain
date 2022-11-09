using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Domain.Core.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IMapper _mapper;

    public GreeterService(ILogger<GreeterService> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        var reply = _mapper.Map<HelloReply>(request);
        // return Task.FromResult(new HelloReply
        // {
        //     Message = "Hello " + request.Name
        // });
        return Task.FromResult(reply);
    }

    public override Task<HelloReplyTwo> SayHelloTwo(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReplyTwo()
        {
            Message = "hello" + request.Name
        });
    }
}