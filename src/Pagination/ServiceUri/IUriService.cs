using EasySharp.Pagination.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Pagination.ServiceUri
{
    public interface IUriService
    {
        Uri GetPageUri(PaginationFilter filter, string route);
    }
}
