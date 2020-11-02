using Mix.Data.Services;
using Mix.Library.Entities.Databases.HouseSites;
using Mix.Library.Repositories.HouseSites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Services
{
    /// <summary>
    /// 宅基地服务
    /// </summary>
    public class HouseSiteService : ApplicationService, IHouseSiteService
    {
        private readonly IJcxxRepository jcxxRepository;
        private readonly ICyxxRepository cyxxRepository;

        public HouseSiteService(IJcxxRepository jcxxRepository, ICyxxRepository cyxxRepository)
        {
            this.jcxxRepository = jcxxRepository;
            this.cyxxRepository = cyxxRepository;
        }

        public async Task InsertOrUpdateJcxxAndCyxx(IEnumerable<Jcxx> jcxxes, IEnumerable<Cyxx> cyxxes)
        {
            foreach (var jcxx in jcxxes)
            {
                await jcxxRepository.InsertOrUpdateAsync(jcxx);
            }
            foreach (var cyxx in cyxxes)
            {
                await cyxxRepository.InsertOrUpdateAsync(cyxx);
            }
        }
    }
}