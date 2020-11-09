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
        /// <summary>
        /// Initializes a new instance of the <see cref="JcxxRepository"/> class.
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="currentUser"></param>
        public JcxxRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }
    }
}