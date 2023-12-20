using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ResponseModels
{
    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> data, IPagination pagination)
        {
            Data = data;
            Pagination = pagination;
        }

        public IEnumerable<T> Data { get; set; }
        public IPagination Pagination { get; set; }
    }

    
}
