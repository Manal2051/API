using AutoMapper;
using TestToken.DTO;
using TestToken.DTO.EmployeeDto;
using TestToken.DTO.UserDtos;
using TestToken.Models;

namespace TestToken.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, RegisterDto>().ReverseMap();
            CreateMap<ApplicationUser, userDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }

    }
}
