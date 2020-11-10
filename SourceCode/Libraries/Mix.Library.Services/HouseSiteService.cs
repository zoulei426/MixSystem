using Mix.Data.Services;
using Mix.Library.Entities.Databases.HouseSites;
using Mix.Library.Repositories.HouseSites;
using System.Collections.Generic;
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
        private readonly INfxxRepository nfxxRepository;

        public HouseSiteService(IJcxxRepository jcxxRepository, ICyxxRepository cyxxRepository, INfxxRepository nfxxRepository)
        {
            this.jcxxRepository = jcxxRepository;
            this.cyxxRepository = cyxxRepository;
            this.nfxxRepository = nfxxRepository;
        }

        public async Task<IEnumerable<Cyxx>> GetCyxxesAsync()
        {
            return await cyxxRepository.Select.ToListAsync();
        }

        public async Task<IEnumerable<Jcxx>> GetJcxxesAsync()
        {
            return await jcxxRepository.Select.ToListAsync();
        }

        public async Task<IEnumerable<Nfxx>> GetNfxxesAsync()
        {
            return await nfxxRepository.Select.ToListAsync();
        }

        /// <summary>
        /// Inserts the or update JCXX and cyxx.
        /// </summary>
        /// <param name="jcxxes">The jcxxes.</param>
        /// <param name="cyxxes">The cyxxes.</param>
        /// <returns></returns>
        public async Task InsertOrUpdateJcxxAndCyxx(IEnumerable<Jcxx> jcxxes, IEnumerable<Cyxx> cyxxes)
        {
            foreach (var jcxx in jcxxes)
            {
                var item = await jcxxRepository.Where(t => t.Hzxm.Equals(jcxx.Hzxm) && t.Zjhm.Equals(jcxx.Zjhm)).FirstAsync();
                if (item is not null)
                {
                    item.Dzxq = jcxx.Dzxq;
                    item.Jtrs = jcxx.Jtrs;
                    item.Sjhm = jcxx.Sjhm;
                    await jcxxRepository.UpdateAsync(item);
                }
                else
                {
                    await jcxxRepository.InsertAsync(jcxx);
                }
            }
            foreach (var cyxx in cyxxes)
            {
                var item = await cyxxRepository.Where(t => t.Xm.Equals(cyxx.Xm) && t.Zjhm.Equals(cyxx.Zjhm)).FirstAsync();
                if (item is not null)
                {
                    item.Xb = cyxx.Xb;
                    await cyxxRepository.UpdateAsync(item);
                }
                else
                {
                    await cyxxRepository.InsertAsync(cyxx);
                }
            }
        }
    }
}