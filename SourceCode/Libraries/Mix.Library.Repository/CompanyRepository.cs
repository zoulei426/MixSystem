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
    public class CompanyRepository : AuditBaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }
    }
}