using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Pagination.Wrappers
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchQuery { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
            this.SearchQuery = "";
        }
        public PaginationFilter(int pageNumber, int pageSize, string Frontfilter)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
            this.SearchQuery = Frontfilter;
        }
    }
}
