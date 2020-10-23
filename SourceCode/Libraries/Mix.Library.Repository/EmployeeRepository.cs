using FreeSql;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases;

namespace Mix.Library.Repositories
{
    public class EmployeeRepository : AuditBaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }
    }
}