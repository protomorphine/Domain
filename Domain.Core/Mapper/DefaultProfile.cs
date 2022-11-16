using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Services;

namespace Domain.Core.Mapper;

public class DefaultProfile : Profile
{
    public DefaultProfile()
    {
        CreateMap<CreateDto, ExampleEntity>();

        CreateMap<ExampleEntity, ExampleEntityDto>();

        CreateMap<UpdateDto, ExampleEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}