using AutoMapper;
using Povorot.Api.Dto;
using Povorot.DAL.Models;

namespace Povorot.Mappers
{
    public class MapperInitializer: Profile
    {
        public MapperInitializer()
        {
            CreateMap<CarStation, CarStationCreateDto>().ReverseMap();
            CreateMap<CarStation, CarStationDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RepairPost, RepairPostDto>().ReverseMap();
            CreateMap<RepairPost, RepairPostCreateDto>().ReverseMap();
            CreateMap<CarStation, IdNameDto>();
            CreateMap<Mechanic, MechanicDto>().ReverseMap();
            CreateMap<Mechanic, MechanicCreateDto>();
            CreateMap<WorkCategory, WorkCategoryDto>().ReverseMap();
            CreateMap<WorkCategory, WorkCategoryCreateDto>().ReverseMap();
        }
    }
}
