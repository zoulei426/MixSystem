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
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyAddDto, Company>();
        }
    }
}