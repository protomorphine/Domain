using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Domain.Core.Services;

public class ExampleService : Example.ExampleBase
{
    private readonly IExampleEntityRepository _exampleEntityRepository;

    private readonly IMapper _mapper;

    public ExampleService(IExampleEntityRepository exampleEntityRepository, IMapper mapper)
    {
        _exampleEntityRepository = exampleEntityRepository;
        _mapper = mapper;
    }

    public override async Task<ExampleEntityDto> CreateAsync(CreateDto request, ServerCallContext context)
    {
        var entity = _mapper.Map<ExampleEntity>(request);
        await _exampleEntityRepository.CreateAsync(entity);
        return _mapper.Map<ExampleEntityDto>(entity);
    }

    public override async Task<ExampleEntityDto> UpdateAsync(UpdateDto request, ServerCallContext context)
    {
        var entity = await _exampleEntityRepository.GetAsync(request.Id);

        var updated = _mapper.Map(request, entity);

        await _exampleEntityRepository.UpdateAsync(updated);

        return _mapper.Map<ExampleEntityDto>(updated);
    }

    public override async Task<ExampleEntityDto> GetAsync(GetEntity request, ServerCallContext context)
    {
        var entity = await _exampleEntityRepository.GetAsync(request.Id);

        return _mapper.Map<ExampleEntityDto>(entity);
    }

    public override async Task<EntityListDto> GetAllAsync(GetAll request, ServerCallContext context)
    {
        var entities = await _exampleEntityRepository.GetAllAsync();

        var reply = new EntityListDto();
        reply.Entities.AddRange(entities.Select(it => _mapper.Map<ExampleEntityDto>(it)));
        return reply;
    }

    public override async Task<Empty> DeleteAsync(DeleteDto request, ServerCallContext context)
    {
        var entity = await _exampleEntityRepository.GetAsync(request.Id);
        await _exampleEntityRepository.DeleteAsync(entity);

        return new Empty();
    }
}