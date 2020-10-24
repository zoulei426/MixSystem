using Mix.Core;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Repositories
{
    public interface IEmployeeRepository : IAuditBaseRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string genderDisplay, string q);

        Task<Employee> AddEmployeeAsync(Guid companyId, Employee employee);
    }
}