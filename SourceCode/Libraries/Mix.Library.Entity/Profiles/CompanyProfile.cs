using AutoMapper;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.Models;

namespace Mix.Library.Entities.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>();
        }
    }
}