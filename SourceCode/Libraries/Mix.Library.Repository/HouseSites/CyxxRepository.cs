using FreeSql;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases.HouseSites;

namespace Mix.Library.Repositories.HouseSites
{
    /// <summary>
    /// Cyxx Repository
    /// </summary>
    public class CyxxRepository : AuditBaseRepository<Cyxx>, ICyxxRepository
    {
        public CyxxRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }
    }
}
