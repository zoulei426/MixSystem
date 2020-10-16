using System;

namespace Mix.Core
{
    public interface IEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        Guid Id { get; set; }
    }
}