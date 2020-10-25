using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Data.Dtos
{
    /// <summary>
    /// Dto Parameters
    /// </summary>
    public class DtoParameters
    {
        /// <summary>
        /// The maximum page size
        /// </summary>
        protected const int MAX_PAGE_SIZE = 100;

        /// <summary>
        /// The default page number
        /// </summary>
        protected const int DEFAULT_PAGE_NUMBER = 1;

        /// <summary>
        /// The default page size
        /// </summary>
        protected const int DEFAULT_PAGE_SIZE = 10;

        #region Fields

        private int _pageSize;

        #endregion Fields

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DtoParameters"/> class.
        /// </summary>
        public DtoParameters()
        {
            PageNumber = DEFAULT_PAGE_NUMBER;
            PageSize = DEFAULT_PAGE_SIZE;
        }
    }
}