using Application.Implementation;
using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class BaseQuery
    {

        public string SearchText { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public IPagination ToPagination() => new Pagination(PageNo, PageSize);

    }
}