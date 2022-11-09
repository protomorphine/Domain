using AutoMapper;
using Domain.Core.Entities;
using Grpc.Core;

namespace Domain.Core.Services;

public class CrudService : Crud.CrudBase
{
    private readonly IMapper _mapper;

    public CrudService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public override Task<ExampleEntityDto> Create(CreateDto request, ServerCallContext context)
    {
        var entity = new ExampleEntityDto()
        {
            Id = 1,
        };

        return Task.FromResult(_mapper.Map(request, entity));
    }
}