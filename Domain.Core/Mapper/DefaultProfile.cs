using AutoMapper;
using Domain.Core.Services;

namespace Domain.Core.Mapper;

public class DefaultProfile : Profile
{
    public DefaultProfile()
    {
        CreateMap<HelloRequest, HelloReply>();

        CreateMap<CreateDto, ExampleEntityDto>();
    }
}