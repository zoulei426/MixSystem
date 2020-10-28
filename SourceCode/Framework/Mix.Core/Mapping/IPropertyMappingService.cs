using System.Collections.Generic;

namespace Mix.Core.Mapping
{
    /// <summary>
    /// 属性映射服务
    /// </summary>
    public interface IPropertyMappingService
    {
        /// <summary>
        /// Gets the property mapping.
        /// </summary>
        /// <typeparam name="TSouce">The type of the souce.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <returns></returns>
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSouce, TDestination>();
    }
}