using System;
using System.ComponentModel.DataAnnotations;

namespace Mix.Core
{
    /// <summary>
    /// 数据库实体
    /// </summary>
    [Serializable]
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        public Guid ID { get; set; }
    }
}