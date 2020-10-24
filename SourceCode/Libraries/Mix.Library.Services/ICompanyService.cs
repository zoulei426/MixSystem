using Mix.Data.Services;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto> CreateCompanyAsync(CompanyAddDto company);

        Task<IEnumerable<CompanyDto>> CreateCompanyCollectionAsync(IEnumerable<CompanyAddDto> companieCollection);
    }
}