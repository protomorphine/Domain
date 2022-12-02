using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Services;
using Google.Protobuf.WellKnownTypes;

namespace Domain.Core.Mapper;

public class DefaultProfile : Profile
{
    public DefaultProfile()
    {
        CreateMap<CreateRequest, ExampleEntity>();

        CreateMap<ExampleEntity, ExampleEntityReply>()
            .ForMember(dest => dest.CreatedAt, opt =>
                opt.MapFrom(src =>
                    Timestamp.FromDateTime(DateTime.SpecifyKind(src.CreatedAt, DateTimeKind.Utc))))
            .ForMember(dest => dest.UpdatedAt, opt =>
                opt.MapFrom(src =>
                    Timestamp.FromDateTime(DateTime.SpecifyKind(src.UpdatedAt, DateTimeKind.Utc))));

        CreateMap<UpdateRequest, ExampleEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt =>
                opt.MapFrom(src => src.UpdatedAt.ToDateTime()));
    }
}