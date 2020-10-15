using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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