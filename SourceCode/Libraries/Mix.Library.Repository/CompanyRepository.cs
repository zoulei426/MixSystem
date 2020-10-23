using FreeSql;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases;

namespace Mix.Library.Repositories
{
    public class CompanyRepository : AuditBaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }
    }
}