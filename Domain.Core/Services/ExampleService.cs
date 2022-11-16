using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Extensions;
using Domain.Core.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

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

    private readonly ILogger<ExampleService> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Create new instance of <see cref="ExampleService"/>
    /// </summary>
    /// <param name="exampleEntityRepository">repository to work with <see cref="ExampleEntity"/></param>
    /// <param name="mapper">mapper</param>
    /// <param name="logger">logger</param>
    public ExampleService(IExampleEntityRepository exampleEntityRepository, IMapper mapper, ILogger<ExampleService> logger)
    {
        _exampleEntityRepository = exampleEntityRepository;
        _mapper = mapper;
        _logger = logger;
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

        _logger.LogInformation("Entity with id {Id} was successfully created", entity!.Id);

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

        _logger.LogInformation("Entity with id {Id} was successfully updated", entity!.Id);

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

        var reply = new EntityListDto {Count = await _exampleEntityRepository.GetCount()};
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

        _logger.LogInformation("Entity with id {Id} was successfully deleted", entity!.Id);

        return new Empty();
    }

    #endregion


}