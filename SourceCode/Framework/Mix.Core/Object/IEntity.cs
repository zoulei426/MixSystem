using System;

namespace Mix.Core
{
    /// <summary>
    /// IEntity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        Guid Id { get; set; }
    }
}