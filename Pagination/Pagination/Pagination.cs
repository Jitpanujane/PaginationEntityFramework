using Pagination.Pagination.Installer;
using Pagination.Pagination.Condition;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Pagination.MultipleResultStoreProc;
using System.Web;

namespace Pagination.Pagination
{
    public class Pagination_v2
    {
        private DbContext db;
        private int page = 0;
        private int pagelimit = 0;
        private string filter = "";
        private string name = "";
        public Pagination_v2(DbContext db_)
        {
            db = db_;
            Starter.go(db);
        }
        public Response<T> QueryPagination<T>(string TableOrView, SQLWhere where = null, params SQLSort[] sort)
        {
            GetParamsRequest();
            string get_where = where == null ? "" : where.GetWhere;
            string get_column = name;
            string get_value = filter.Replace("'", "''");
            string get_sort = GetSort(sort);
            var res = db.Database.SqlQuery<resJson>("EXEC [dbo].[s_PaginationJSON] @TableOrView,@Where,@FilterColumn,@FilterValue,@Page,@Limit_page,@Sortby",
                   new SqlParameter("@TableOrView", TableOrView),
                   new SqlParameter("@Where", get_where),
                   new SqlParameter("@FilterColumn", get_column),
                   new SqlParameter("@FilterValue", get_value),
                   new SqlParameter("@Page", page),
                   new SqlParameter("@Limit_page", pagelimit),
                   new SqlParameter("@Sortby", get_sort)
               ).FirstOrDefault();
            if (res.Item != null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(res.Item);
                return new Response<T>
                {
                    currentPage = page,
                    limitRow = pagelimit,
                    resultRow = res.Count_row,
                    items = result
                };
            }
            else
            {
                return new Response<T>
                {
                    currentPage = page,
                    limitRow = pagelimit,
                    resultRow = res.Count_row,
                    items = new T[] { }
                };
            }

        }
        public ResponseJSON QueryPagination(string TableOrView, SQLWhere where = null, params SQLSort[] sort)
        {
            GetParamsRequest();
            string get_where = where == null ? "" : where.GetWhere;
            string get_column = name;
            string get_value = filter.Replace("'", "''");
            string get_sort = GetSort(sort);
            var res = db.Database.SqlQuery<resJson>("EXEC [dbo].[s_PaginationJSON] @TableOrView,@Where,@FilterColumn,@FilterValue,@Page,@Limit_page,@Sortby",
                   new SqlParameter("@TableOrView", TableOrView),
                   new SqlParameter("@Where", get_where),
                   new SqlParameter("@FilterColumn", get_column),
                   new SqlParameter("@FilterValue", get_value),
                   new SqlParameter("@Page", page),
                   new SqlParameter("@Limit_page", pagelimit),
                   new SqlParameter("@Sortby", get_sort)
               ).FirstOrDefault();
            if (res.Item != null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(res.Item);
                return new ResponseJSON
                {
                    currentPage = page,
                    limitRow = pagelimit,
                    resultRow = res.Count_row,
                    items = result
                };
            }
            else
            {
                return new ResponseJSON
                {
                    currentPage = page,
                    limitRow = pagelimit,
                    resultRow = res.Count_row,
                    items = new object[] { }
                };
            }
        }

        public IEnumerable<T> Query<T>(string TableOrView, SQLWhere where = null, params SQLSort[] sort)
        {
            GetParamsRequest();
            string get_where = where == null ? "" : where.GetWhere;
            string get_column = name;
            string get_value = filter.Replace("'", "''");
            string get_sort = GetSort(sort);
            var res = db.Database.SqlQuery<resJson>("EXEC [dbo].[s_PaginationJSON] @TableOrView,@Where,@FilterColumn,@FilterValue,@Page,@Limit_page,@Sortby",
                    new SqlParameter("@TableOrView", TableOrView),
                    new SqlParameter("@Where", get_where),
                    new SqlParameter("@FilterColumn", get_column),
                    new SqlParameter("@FilterValue", get_value),
                    new SqlParameter("@Page", DBNull.Value),
                    new SqlParameter("@Limit_page", DBNull.Value),
                    new SqlParameter("@Sortby", get_sort)
                ).FirstOrDefault();
            if (res.Item != null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(res.Item);
                return result;
            }
            return new T[] { };
        }

        public object QueryJSON(string TableOrView, SQLWhere where = null, params SQLSort[] sort)
        {
            GetParamsRequest();
            string get_where = where == null ? "" : where.GetWhere;
            string get_column = name;
            string get_value = filter.Replace("'", "''");
            string get_sort = GetSort(sort);
            var res = db.Database.SqlQuery<resJson>("EXEC [dbo].[s_PaginationJSON] @TableOrView,@Where,@FilterColumn,@FilterValue,@Page,@Limit_page,@Sortby",
                    new SqlParameter("@TableOrView", TableOrView),
                    new SqlParameter("@Where", get_where),
                    new SqlParameter("@FilterColumn", get_column),
                    new SqlParameter("@FilterValue", get_value),
                    new SqlParameter("@Page", DBNull.Value),
                    new SqlParameter("@Limit_page", DBNull.Value),
                    new SqlParameter("@Sortby", get_sort)
                ).FirstOrDefault();
            if (res.Item != null)
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(res.Item);
                return result;
            }
            return new object[] { };
        }
        private string GetSort(SQLSort[] sort)
        {
            string res = "";
            int i = 0;
            foreach (var sorts in sort)
            {
                if (i == 0)
                    res += sorts.GetSort;
                else
                    res += " , " + sorts.GetSort;
                i++;
            }
            return res;
        }
        private void GetParamsRequest()
        {
            HttpRequest Request = HttpContext.Current.Request;
            page = string.IsNullOrEmpty(Request["page"]) ? 1 : int.Parse(Request["page"]);
            pagelimit = string.IsNullOrEmpty(Request["limit"]) ? 15 : int.Parse(Request["limit"]);
            filter = string.IsNullOrEmpty(Request["filter"]) ? "" : Request["filter"];
            name = string.IsNullOrEmpty(Request["name"]) ? "" : Request["name"];
        }

    }
    public class resJson
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public int Count_row { get; set; }
    }
    public class resObject
    {
        public int Id { get; set; }
        public int Count_row { get; set; }
    }

}
