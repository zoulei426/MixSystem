using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Data.Pagable
{
    public class PaginationMetadata
    {
        public const string KEY = "X-Pagination";

        public long TotalCount { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public long TotalPages { get; set; }

        public string PreviousPageLink { get; set; }

        public string NextPageLink { get; set; }
    }
}
