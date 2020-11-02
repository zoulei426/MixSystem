using System;

namespace Mix.Core
{
    /// <summary>
    /// IEntity
    /// </summary>
    public interface IEntity<T>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        T Id { get; set; }
    }
}