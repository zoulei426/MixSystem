using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Data.Pagable
{
    /// <summary>
    /// ResourceUriType
    /// </summary>
    public enum ResourceUriType
    {
        /// <summary>
        /// The previous page
        /// </summary>
        PreviousPage,

        /// <summary>
        /// The next page
        /// </summary>
        NextPage
    }
}