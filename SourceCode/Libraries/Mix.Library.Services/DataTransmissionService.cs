using Mix.Core;
using Mix.Data.Repositories;
using Mix.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Services
{
    /// <summary>
    /// 数据传输服务
    /// </summary>
    public class DataTransmissionService<T, TIdType> : ApplicationService where T : IEntity<TIdType>
    {
        
    }
}
