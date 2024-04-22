using AutoMapper;
using ContactHub.Model.DTOs;
using ContactHub.Model.Entity;
using System.Runtime;

namespace ContactHub.Mappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, UserToReturnDTO>().ReverseMap();
            CreateMap<UserToAddDTO, User>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
        }
    }
}
