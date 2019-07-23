using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Pagination
{   
    public static class PaginationFilterOnModel
    {
        // Pagination สำหรับที่ต้องใช้ Parameter
        public static Pagination<T> Pagination<T>(this IEnumerable<T> Items, int startRow, int limitRow)
        {
            Pagination<T> pagination = new Pagination<T>();
            T[] convertItem = Items.ToArray();
            pagination.items = convertItem
                .Skip((startRow > 1 ? (startRow - 1) : 0) * limitRow)
                .Take(limitRow);
            pagination.currentPage = startRow;
            pagination.limitRow = limitRow;
            pagination.resultRow = convertItem.Length;
            //pagination.url = HttpContext.Current.Request.Url.AbsolutePath.Replace("/api/", "");
            return pagination;
        }

        // Pagination สำหรับที่ไม่ต้องใช้ Parameter
        public static Pagination<T> Pagination<T>(this IEnumerable<T> Items)
        {
            HttpRequest Request = HttpContext.Current.Request;
            // ตรวจสอบว่า Convert ได้หรือไม่
            int ARF_PAGE = string.IsNullOrEmpty(Request["page"]) ? 1 : int.Parse(Request["page"]);
            int ARF_LIMIT = string.IsNullOrEmpty(Request["limit"]) ? 15 : int.Parse(Request["limit"]);
            return Items.Pagination<T>(ARF_PAGE, ARF_LIMIT);
        }

        // Search สำหรับที่ต้องใช้ Parameter
        public static IEnumerable<T> Search<T>(this IEnumerable<T> Items, string value, params string[] keys)
        {
            Func<T, bool> onSearch = (item) =>
            {
                if (string.IsNullOrEmpty(value)) return true;
                bool[] isFilter = new bool[keys.Length];
                int index = 0;
                foreach (var key in keys)
                {
                    object searchText = item.GetType().GetProperty(key).GetValue(item) ?? "";
                    isFilter[index] = searchText.ToString().ToUpper().Contains(value.ToUpper());
                    index += 1;
                }
                return isFilter.FirstOrDefault(m => m == true); 
            };
            return Items.Where(m => onSearch(m));
        }

        // Search สำหรับที่ไม่ต้องใช้ Parameter
        public static IEnumerable<T> Search<T>(this IEnumerable<T> Items, params string[] keys)
        {
            HttpRequest Request = HttpContext.Current.Request;
            string ARF_FILTER = string.IsNullOrEmpty(Request["filter"]) ? "" : Request["filter"];
            return Items.Search<T>(ARF_FILTER, keys);
        }

        // Pagination และ Search ไปพรอ้มกัน ที่ใส่แค่ key ที่จะ Search เท่านั้น
        public static Pagination<T> PaginationSearch<T>(this IEnumerable<T> Items, params string[] keysFilter)
        {
            var convertItem = Items.ToArray();
            var onFilter = convertItem.Search(keysFilter).ToArray();
            var onPagination = onFilter.Pagination<T>();
            //onPagination.searchRow = onFilter.Length;
            onPagination.resultRow = convertItem.Length;
            return onPagination;
        }
    }
}