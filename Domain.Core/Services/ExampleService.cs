using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Extensions;
using Domain.Core.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Domain.Core.Services;

public class ExampleService : Example.ExampleBase
{
    #region Fields

    /// <summary>
    /// Repository to work with <see cref="ExampleEntity"/>
    /// </summary>
    private readonly IExampleEntityRepository _exampleEntityRepository;

    /// <summary>
    /// Mapper
    /// </summary>
    private readonly IMapper _mapper;

    #endregion

    #region Constructors

    /// <summary>
    /// Create new instance of <see cref="ExampleService"/>
    /// </summary>
    /// <param name="exampleEntityRepository">repository to work with <see cref="ExampleEntity"/></param>
    /// <param name="mapper">mapper</param>
    public ExampleService(IExampleEntityRepository exampleEntityRepository, IMapper mapper)
    {
        _exampleEntityRepository = exampleEntityRepository;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Create new <see cref="ExampleEntity"/> in database
    /// </summary>
    /// <param name="request">data to create entity</param>
    /// <param name="context">gRPC context</param>
    /// <returns>DTO of created entity</returns>
    public override async Task<ExampleEntityDto> CreateAsync(CreateDto request, ServerCallContext context)
    {
        var entity = _mapper.Map<ExampleEntity>(request);
        await _exampleEntityRepository.CreateAsync(entity);
        return _mapper.Map<ExampleEntityDto>(entity);
    }

    /// <summary>
    /// Update existing entity
    /// </summary>
    /// <param name="request">data to update entity</param>
    /// <param name="context">gRPC context</param>
    /// <returns>DTO of updated entity</returns>
    public override async Task<ExampleEntityDto> UpdateAsync(UpdateDto request, ServerCallContext context)
    {
        var entity = await _exampleEntityRepository.GetAsync(request.Id);

        entity.ThrowRpcEntityNotFoundIfNull($"Entity with id = {request.Id} not found.");

        var updated = _mapper.Map(request, entity);

        await _exampleEntityRepository.UpdateAsync(updated!);

        return _mapper.Map<ExampleEntityDto>(updated);
    }

    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="request">entity id</param>
    /// <param name="context">gRPC context</param>
    /// <returns>DTO of requested entity</returns>
    public override async Task<ExampleEntityDto> GetAsync(Int64Value request, ServerCallContext context)
    {
        var entity = await _exampleEntityRepository.GetAsync(request.Value);

        entity.ThrowRpcEntityNotFoundIfNull($"Entity with id = {request.Value} not found.");

        return _mapper.Map<ExampleEntityDto>(entity);
    }

    /// <summary>
    /// Gets all entities from database
    /// </summary>
    /// <param name="request">empty request<see cref="Empty"/></param>
    /// <param name="context">gRPC context</param>
    /// <returns>List of entities</returns>
    public override async Task<EntityListDto> GetAllAsync(Empty request, ServerCallContext context)
    {
        var entities = await _exampleEntityRepository.GetAllAsync();

        var reply = new EntityListDto();
        reply.Entities.AddRange(entities.Select(it => _mapper.Map<ExampleEntityDto>(it)));
        return reply;
    }

    /// <summary>
    /// Delete entity by id
    /// </summary>
    /// <param name="request">entity id</param>
    /// <param name="context">gRPC context</param>
    /// <returns><see cref="Empty"/></returns>
    public override async Task<Empty> DeleteAsync(Int64Value request, ServerCallContext context)
    {
        var entity = await _exampleEntityRepository.GetAsync(request.Value);

        entity.ThrowRpcEntityNotFoundIfNull($"Entity with id = {request.Value} not found.");

        await _exampleEntityRepository.DeleteAsync(entity!);

        return new Empty();
    }

    #endregion


}