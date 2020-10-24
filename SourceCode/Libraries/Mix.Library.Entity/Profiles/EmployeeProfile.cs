using AutoMapper;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entities.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName}{src.LastName}"))
                .ForMember(dest => dest.GenderDisplay, opt => opt.MapFrom(src => src.Gender.GetCustomAttributeDescription()))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.Age()));

            CreateMap<EmployeeAddDto, Employee>();
        }
    }
}