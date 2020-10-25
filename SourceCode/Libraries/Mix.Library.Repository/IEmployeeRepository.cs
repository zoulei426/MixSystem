using Mix.Data.Repositories;
using Mix.Library.Entities.Databases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mix.Library.Repositories
{
    /// <summary>
    /// IEmployeeRepository
    /// </summary>
    /// <seealso cref="Mix.Data.Repositories.IAuditBaseRepository{T}" />
    public interface IEmployeeRepository : IAuditBaseRepository<Employee>
    {
        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="genderDisplay">The gender display.</param>
        /// <param name="q">The q.</param>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string genderDisplay, string q);

        /// <summary>
        /// Adds the employee asynchronous.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        Task<Employee> AddEmployeeAsync(Guid companyId, Employee employee);
    }
}