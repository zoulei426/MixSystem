using Mix.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Guid CreaterID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        public Guid? ModifierID { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModityTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        public Guid? DeleterID { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }
}