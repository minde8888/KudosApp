using AutoMapper;
using Kudos.Domain.Entities;
using Kudos.Services.Dtos;

namespace Kudos.Services.Services.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Kudo, KudoRequest>().ReverseMap();
            CreateMap<Kudo, KudoResult>().ReverseMap();
            CreateMap<Employee, EmployeeRequest>().ReverseMap();
        }       
    }
}
