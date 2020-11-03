using Mix.Core;
using Mix.Data.Services;

namespace Mix.Library.Services
{
    /// <summary>
    /// 数据传输服务
    /// </summary>
    public class DataTransmissionService<T, TIdType> : ApplicationService where T : IEntity<TIdType>
    {

    }
}
