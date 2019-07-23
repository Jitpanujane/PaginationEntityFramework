using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.Pagination.Condition
{
    public class SQLWhere
    {
        public string GetWhere { get; private set; }
        /// <summary>
        /// Please input Where string in petten { [columnName] [ = || != || LIKE ] [value] } example { Id = 1 }
        /// </summary>
        /// <param name="whereString">[columnName] [ = || != || LIKE ] [value]</param>
        public SQLWhere(string whereString) {
            GetWhere = "WHERE " + whereString.Replace("WHERE", "");
        }
    }
}
