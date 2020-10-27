using AutoMapper;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.Dtos;

namespace Mix.Library.Entities.Profiles
{
    /// <summary>
    /// CompanyProfile
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class CompanyProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyProfile"/> class.
        /// </summary>
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyAddDto, Company>();
            CreateMap<Company, CompanyFullDto>();
        }
    }
}