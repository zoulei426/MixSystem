using FreeSql;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases.HouseSites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Repositories.HouseSites
{
    /// <summary>
    /// Jcxx Repository
    /// </summary>
    public class JcxxRepository : AuditBaseRepository<Jcxx>, IJcxxRepository
    {
        public JcxxRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }

       
    }
}
