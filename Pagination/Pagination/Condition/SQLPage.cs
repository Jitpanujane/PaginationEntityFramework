using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.Pagination.Condition
{
    public class SQLPage
    {
        public int GetPage { get; private set; }
        public int GetLimitPage { get; private set; }
        /// <summary>
        /// SQLPage is param for Pagination [Page] = page now and [LimitPage] = your limit page
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="LimitPage"></param>
        public SQLPage(int Page,int LimitPage) {
            GetPage = Page;
            GetLimitPage = LimitPage;
        }
    }
}
