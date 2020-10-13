using System;
using System.Collections.Generic;
using System.Text;

namespace Mix.Data.Entities
{
    /// <summary>
    /// 创建审计
    /// </summary>
    public interface ICreateAduitEntity
    {
        /// <summary>
        /// 创建者ID
        /// </summary>
        Guid CreaterID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 修改审计
    /// </summary>
    public interface IUpdateAuditEntity
    {
        /// <summary>
        /// 最后修改人ID
        /// </summary>
        Guid? ModifierID { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime? ModityTime { get; set; }
    }

    /// <summary>
    /// 删除审计
    /// </summary>
    public interface IDeleteAduitEntity
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        Guid? DeleterID { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeleteTime { get; set; }
    }
}