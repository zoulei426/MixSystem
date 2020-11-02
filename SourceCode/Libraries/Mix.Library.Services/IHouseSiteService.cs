using Mix.Library.Entities.Databases.HouseSites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mix.Library.Services
{
    public interface IHouseSiteService
    {
        Task InsertOrUpdateJcxxAndCyxx(IEnumerable<Jcxx> jcxxes, IEnumerable<Cyxx> cyxxes);
    }
}