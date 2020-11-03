using FreeSql;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases.HouseSites;

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
