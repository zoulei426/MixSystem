using System.Collections.Generic;

namespace Mix.Core.Mapping
{
    /// <summary>
    /// 属性映射值
    /// </summary>
    public class PropertyMappingValue
    {
        /// <summary>
        /// 目标映射集合
        /// </summary>
        /// <value>
        /// The destination properties.
        /// </value>
        public IEnumerable<string> DestinationProperties { get; set; }

        /// <summary>
        /// 是否反转 <see cref="PropertyMappingValue"/> is revert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if revert; otherwise, <c>false</c>.
        /// </value>
        public bool Revert { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMappingValue"/> class.
        /// </summary>
        /// <param name="destinationProperties">The destination properties.</param>
        /// <param name="revert">if set to <c>true</c> [revert].</param>
        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
        {
            Guards.ThrowIfNull(destinationProperties);

            DestinationProperties = destinationProperties;
            Revert = revert;
        }
    }
}