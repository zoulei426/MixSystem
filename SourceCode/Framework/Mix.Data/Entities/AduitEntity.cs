using FreeSql.DataAnnotations;
using Mix.Core;
using System;

namespace Mix.Data.Entities
{
    /// <summary>
    /// 审计实体
    /// </summary>
    [Serializable]
    public class AduitEntity : Entity, IUpdateAuditEntity, IDeleteAduitEntity, ICreateAduitEntity
    {
        /// <summary>
        /// 创建者ID
        /// </summary>
        [Column(Position = -7)]
        public Guid CreaterId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(Position = -6)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Column(Position = -5)]
        public Guid? ModifierId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column(Position = -4)]
        public DateTime? ModityTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column(Position = -3)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        [Column(Position = -2)]
        public Guid? DeleterId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column(Position = -1)]
        public DateTime? DeleteTime { get; set; }
    }
}