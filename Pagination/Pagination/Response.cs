using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.Pagination
{
    public class Response<T>
    {
        public IEnumerable<T> items { get; set; }
        public int currentPage { get; set; }
        public int limitRow { get; set; }
        public int resultRow { get; set; }
    }
}
