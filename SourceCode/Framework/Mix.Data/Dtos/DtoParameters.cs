using System.Collections;

namespace Mix.Data.Dtos
{
    /// <summary>
    /// Dto Parameters
    /// </summary>
    public class DtoParameters
    {
        #region Consts

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

        /// <summary>
        /// The default order by
        /// </summary>
        protected const string DEFAULT_ORDER_BY = "Name";

        #endregion Consts

        #region Fields

        private int _pageSize;

        #endregion Fields

        #region Properties

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
        /// Gets or sets the order by.
        /// </summary>
        /// <value>
        /// The order by.
        /// </value>
        public string OrderBy { get; set; }

        /// <summary>
        /// 需要获取的字段
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public string Fields { get; set; }

        #endregion Properties

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="DtoParameters"/> class.
        /// </summary>
        public DtoParameters()
        {
            PageNumber = DEFAULT_PAGE_NUMBER;
            PageSize = DEFAULT_PAGE_SIZE;
            OrderBy = DEFAULT_ORDER_BY;
        }

        #endregion Ctor
    }
}