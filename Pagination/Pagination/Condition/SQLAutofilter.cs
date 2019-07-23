using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.Pagination.Condition
{
    
    public class SQLAutofilter
    {
        public string GetColumnName { get; private set; }
        public string GetValue { get; private set; }
        /// <summary>
        /// SQLAutofilter is param
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public SQLAutofilter(string columnName, string value) {
            GetColumnName = columnName;
            GetValue = value;
        }
    }
}
