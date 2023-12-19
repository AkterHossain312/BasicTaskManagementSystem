using Application.Interface;
using Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementation
{
    class Pagination : IPagination
    {
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public int Total { get; set; }

        public Pagination()
        {
            PageSize = int.MaxValue;
        }
        public Pagination(int pageNo, int pageSize)
        {
            PageNo = pageNo - 1;
            PageSize = pageSize;
        }
        public Pagination(int total, int pageNo, int pageSize)
        {
            PageNo = pageNo - 1;
            PageSize = pageSize;
            Total = total;
        }
        public int ToSkip()
        {
            return PageNo * PageSize;
        }

        public int ToTake()
        {
            return PageSize;
        }

        public static Pagination FromQuery(in int total, BaseQuery query)
        {
            return new Pagination(total, query.PageNo + 1, query.PageSize);
        }

        public static Pagination FromQuery(in int total, int pageNo, int pageSize)
        {
            return new Pagination(total, pageNo + 1, pageSize);
        }

        public Pagination Next()
        {
            return new Pagination(this.PageNo + 1, this.PageSize);
        }
    }
}
