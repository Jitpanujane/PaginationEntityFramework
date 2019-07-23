using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.Pagination.Condition
{
    public class SQLSort
    {
        public string GetSort { get; private set; }
        /// <summary>
        /// SQLSort is param for pagination in petten  sortType = { [ASC || DESC] }
        /// </summary>
        /// <param name="columnName">column name</param>
        /// <param name="sortType">ASC || DESC</param>
        public SQLSort(string columnName,string sortType) {
            if (sortType.ToUpper() == "ASC" || sortType.ToUpper() == "DESC")
            {
                GetSort = columnName + " " + sortType;
            }
            else {
                throw new Exception("Your sort type is isvalid.");
            }
        }
    }
}
