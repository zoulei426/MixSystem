using FreeSql;
using Mix.Core;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mix.Library.Repositories
{
    /// <summary>
    /// EmployeeRepository
    /// </summary>
    /// <seealso cref="Mix.Data.Repositories.AuditBaseRepository{T}" />
    /// <seealso cref="Mix.Library.Repositories.IEmployeeRepository" />
    public class EmployeeRepository : AuditBaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="currentUser"></param>
        public EmployeeRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }

        /// <summary>
        /// Gets the employees asynchronous.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="genderDisplay">The gender display.</param>
        /// <param name="q">The q.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string genderDisplay, string q)
        {
            Guards.ThrowIfNull(companyId);

            var query = Select.Where(t => t.CompanyId.Equals(companyId));

            if (genderDisplay.IsNullOrWhiteSpace() && q.IsNullOrWhiteSpace())
            {
                return await query.ToListAsync();
            }

            if (genderDisplay.IsNotNullOrWhiteSpace())
            {
                var gender = genderDisplay.Trim().ToEnum<Gender>();
                query = query.Where(t => t.Gender.Equals(gender));
            }

            if (q.IsNotNullOrWhiteSpace())
            {
                q = q.Trim();
                query = query.Where(t => t.EmployeeNo.Contains(q)
                                      || t.FirstName.Contains(q)
                                      || t.LastName.Contains(q));
            }

            return await query.OrderBy(t => t.EmployeeNo).ToListAsync();
        }

        /// <summary>
        /// Adds the employee asynchronous.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <returns></returns>
        public Task<Employee> AddEmployeeAsync(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            return InsertAsync(employee);
        }
    }
}