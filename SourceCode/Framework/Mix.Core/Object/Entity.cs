using System;
using System.ComponentModel.DataAnnotations;

namespace Mix.Core
{
    /// <summary>
    /// 数据库实体
    /// </summary>
    [Serializable]
    public abstract class Entity<T> : IEntity<T>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        public T Id { get; set; }
    }
}