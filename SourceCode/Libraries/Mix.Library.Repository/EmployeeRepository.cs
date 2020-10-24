using FreeSql;
using Mix.Core;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Repositories
{
    public class EmployeeRepository : AuditBaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }

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

        public Task<Employee> AddEmployeeAsync(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            return InsertAsync(employee);
        }
    }
}