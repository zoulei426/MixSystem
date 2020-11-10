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
    public class NfxxRepository : AuditBaseRepository<Nfxx>, INfxxRepository
    {
        public NfxxRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }
    }
}