using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mix.Desktop.Modules.Enterprise
{
    public interface IMixApi
    {
        [Get("/api/companies")]
        Task<HttpResponseMessage> GetCompaniesAsync(CompanyDtoParameters parameters);

        [Get("/api/companies/{companyId}/employees")]
        Task<IEnumerable<EmployeeDto>> GetEmployeesForCompany(Guid companyId, EmployeeDtoParameters parameters);
    }
}