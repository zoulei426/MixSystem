using AutoMapper;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Data.Pagable
{
    /// <summary>
    /// PagedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.List{T}" />
    public class PagedList<T> : List<T> where T : class
    {
        #region Properties

        /// <summary>
        /// 当前页
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// 总页数
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        public int TotalPages { get; private set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; private set; }

        /// <summary>
        /// 总数量
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public long TotalCount { get; private set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has previous; otherwise, <c>false</c>.
        /// </value>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has next; otherwise, <c>false</c>.
        /// </value>
        public bool HasNext => CurrentPage < TotalPages;

        #endregion Properties

        #region Ctor

      
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="count">The count.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        public PagedList(List<T> items, long count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// Creates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static PagedList<T> Create(ISelect<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static async Task<PagedList<T>> CreateAsync(ISelect<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Page(pageNumber, pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static async Task<PagedList<T>> CreateAsync<TInput>(ISelect<TInput> source, int pageNumber, int pageSize, IMapper mapper) where TInput : class
        {
            var count = await source.CountAsync();
            var items = await source.Page(pageNumber, pageSize).ToListAsync();
            return new PagedList<T>(mapper.Map<List<T>>(items), count, pageNumber, pageSize);
        }

        #endregion Methods
    }
}