using Mix.Library.Entities.Databases.HouseSites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mix.Library.Services
{
    /// <summary>
    /// IHouseSiteService
    /// </summary>
    public interface IHouseSiteService
    {
        /// <summary>
        /// Inserts the or update JCXX and cyxx.
        /// </summary>
        /// <param name="jcxxes">The jcxxes.</param>
        /// <param name="cyxxes">The cyxxes.</param>
        /// <returns></returns>
        Task InsertOrUpdateJcxxAndCyxx(IEnumerable<Jcxx> jcxxes, IEnumerable<Cyxx> cyxxes);

        Task<IEnumerable<Jcxx>> GetJcxxesAsync();

        Task<IEnumerable<Cyxx>> GetCyxxesAsync();

        Task<IEnumerable<Nfxx>> GetNfxxesAsync();
    }
}