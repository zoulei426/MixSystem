using AutoMapper;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.Dtos;

namespace Mix.Library.Entities.Profiles
{
    /// <summary>
    /// EmployeeProfile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class EmployeeProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeProfile"/> class.
        /// </summary>
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName}{src.LastName}"))
                .ForMember(dest => dest.GenderDisplay, opt => opt.MapFrom(src => src.Gender.GetCustomAttributeDescription()))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.Age()));

            CreateMap<EmployeeAddDto, Employee>();

            CreateMap<EmployeeUpdateDto, Employee>();

            CreateMap<Employee, EmployeeUpdateDto>();
        }
    }
}