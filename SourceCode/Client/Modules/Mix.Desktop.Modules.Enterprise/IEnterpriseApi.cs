using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mix.Desktop.Modules.Enterprise
{
    public interface IEnterpriseApi
    {
        [Get("/api/companies")]
        //[Headers("Accept: application/vnd.mix.company.friendly+json")]
        Task<HttpResponseMessage> GetCompaniesAsync(CompanyDtoParameters parameters, 
            [Header("Accept")] string accept = "application/vnd.mix.company.friendly+json");

        [Get("/api/companies/{companyId}/employees")]
        Task<IEnumerable<EmployeeDto>> GetEmployeesForCompany(Guid companyId, EmployeeDtoParameters parameters);
    }
}