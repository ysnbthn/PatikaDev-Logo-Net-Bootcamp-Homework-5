using AutoMapper;
using Domain.Entities;
using HW5.Consumer.DTOs;

namespace HW5.Consumer.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post,PostDto>().ReverseMap();
        }
    }
}
