using Mix.Data.Pagable;
using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using System;
using System.Threading.Tasks;

namespace Mix.Library.Services
{
    /// <summary>
    /// IEmployeeService
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets the employees for company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<PagedList<EmployeeDto>> GetEmployeesForCompany(Guid companyId, EmployeeDtoParameters parameters);
    }
}