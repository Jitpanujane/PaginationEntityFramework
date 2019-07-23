using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Pagination
{
    public class PaginationAndFilter
    {
        private DbContext db;
        public PaginationAndFilter(string nameConnectionString)
        {
            db = new DbContext(nameConnectionString);
        }
        //function public
        //normal
        public Pagination<T> queryPagination<T>(string tableORview_name)
        {
            var request = GetParamsRequest();
            return queryPaginationAndFilterYourWhere<T>(new Parame
            {
                table_name = tableORview_name,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow,
            });
        }
        public Pagination<T> queryPagination<T>(string tableORview_name, ConditionString where)
        {
            var request = GetParamsRequest();
            return queryPaginationAndFilterYourWhere<T>(new Parame
            {
                table_name = tableORview_name,
                yourwhere = where.EXEC,
                key_where = where.EXEC_KEY,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow,
            });
        }
        public Pagination<T> queryPagination<T>(string tableORview_name, string sortByColumn, string sort_type)
        {
            var request = GetParamsRequest();
            checkSort_type(sort_type);
            return queryPaginationAndFilterYourWhere<T>(new Parame
            {
                table_name = tableORview_name,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow,
                sortby = sortByColumn,
                sort_type = sort_type
            });
        }
        public Pagination<T> queryPagination<T>(string tableORview_name, ConditionString where, string sortByColumn, string sort_type)
        {
            var request = GetParamsRequest();
            checkSort_type(sort_type);
            return queryPaginationAndFilterYourWhere<T>(new Parame
            {
                table_name = tableORview_name,
                yourwhere = where.EXEC,
                key_where = where.EXEC_KEY,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow,
                sortby = sortByColumn,
                sort_type = sort_type
            });
        }
        public Pagination<T> queryPagination<T>(string tableORview_name, ConditionString[] where)
        {
            var request = GetParamsRequest();
            string EXEC = "";
            string EXEC_KEY = "";
            foreach (var wheres in where)
            {
                EXEC = EXEC + (EXEC != "" ? " AND " : "") + ("(" + wheres.EXEC + ")");
                EXEC_KEY = EXEC_KEY + (EXEC_KEY != "" ? "," : "") + wheres.EXEC_KEY;
            }
            return queryPaginationAndFilterYourWhere<T>(new Parame
            {
                table_name = tableORview_name,
                yourwhere = EXEC,
                key_where = EXEC_KEY,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow,
            });
        }
        public Pagination<T> queryPagination<T>(string tableORview_name, ConditionString[] where, string sortByColumn, string sort_type)
        {
            var request = GetParamsRequest();
            checkSort_type(sort_type);
            string EXEC = "";
            string EXEC_KEY = "";
            foreach (var wheres in where)
            {
                EXEC = EXEC + (EXEC != "" ? " AND " : "") + ("(" + wheres.EXEC + ")");
                EXEC_KEY = EXEC_KEY + (EXEC_KEY != "" ? "," : "") + wheres.EXEC_KEY;
            }
            return queryPaginationAndFilterYourWhere<T>(new Parame
            {
                table_name = tableORview_name,
                yourwhere = EXEC,
                key_where = EXEC_KEY,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow,
                sortby = sortByColumn,
                sort_type = sort_type
            });
        }
        //json

        public PaginationJSON queryPaginationJSON(string tableORview_name)
        {
            var request = GetParamsRequest();
            return queryPaginationAndFilterYourWhereJSON(new Parame
            {
                table_name = tableORview_name,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow,
            });
        }
        public PaginationJSON queryPaginationJSON(string tableORview_name,ConditionString where)
        {
            var request = GetParamsRequest();
            return queryPaginationAndFilterYourWhereJSON(new Parame
            {
                table_name = tableORview_name,
                yourwhere = where.EXEC,
                key_where = where.EXEC_KEY,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow
            });
        }
        
        public PaginationJSON queryPaginationJSON(string tableORview_name,string sortByColumn, string sort_type)
        {
            checkSort_type(sort_type);
            var request = GetParamsRequest();
            return queryPaginationAndFilterYourWhereJSON(new Parame
            {
                table_name = tableORview_name,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow,
                sortby = sortByColumn,
                sort_type = sort_type
            });
        }
        public PaginationJSON queryPaginationJSON(string tableORview_name,ConditionString where, string sortByColumn, string sort_type)
        {
            checkSort_type(sort_type);
            var request = GetParamsRequest();
            return queryPaginationAndFilterYourWhereJSON(new Parame
            {
                table_name = tableORview_name,
                yourwhere = where.EXEC,
                key_where = where.EXEC_KEY,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow,
                sortby = sortByColumn,
                sort_type = sort_type
            });
        }
        public PaginationJSON queryPaginationJSON(string tableORview_name,ConditionString[] where)
        {
            var request = GetParamsRequest();
            string EXEC = "";
            string EXEC_KEY = "";
            foreach (var wheres in where)
            {
                EXEC = EXEC + (EXEC != "" ? " AND " : "") + ("(" + wheres.EXEC + ")");
                EXEC_KEY = EXEC_KEY + (EXEC_KEY != "" ? "," : "") + wheres.EXEC_KEY;
            }
            return queryPaginationAndFilterYourWhereJSON(new Parame
            {
                table_name = tableORview_name,
                yourwhere = EXEC,
                key_where = EXEC_KEY,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow
            });
        }
        public PaginationJSON queryPaginationJSON(string tableORview_name,ConditionString[] where, string sortByColumn, string sort_type)
        {
            var request = GetParamsRequest();
            checkSort_type(sort_type);
            string EXEC = "";
            string EXEC_KEY = "";
            foreach (var wheres in where)
            {
                EXEC = EXEC + (EXEC != "" ? " AND " : "") + ("(" + wheres.EXEC + ")");
                EXEC_KEY = EXEC_KEY + (EXEC_KEY != "" ? "," : "") + wheres.EXEC_KEY;
            }
            return queryPaginationAndFilterYourWhereJSON(new Parame
            {
                table_name = tableORview_name,
                yourwhere = EXEC,
                key_where = EXEC_KEY,
                search_text = request.filter,
                page = request.startRow,
                limit_page = request.limitRow,
                sortby = sortByColumn,
                sort_type = sort_type
            });
        }
        //json non pagination
        public object queryNonPaginationJSON(string tableORview_name)
        {
            var request = GetParamsRequest();
            return queryPaginationAndFilterYourWhereJSONnonPagination(new Parame
            {
                table_name = tableORview_name,
                search_text = request.filter,
                page = 0,
                limit_page = 0,
            });
        }
        public object queryNonPaginationJSON(string tableORview_name, ConditionString where)
        {
            var request = GetParamsRequest();
            return queryPaginationAndFilterYourWhereJSONnonPagination(new Parame
            {
                table_name = tableORview_name,
                yourwhere = where.EXEC,
                key_where = where.EXEC_KEY,
                search_text = request.filter,
                page = 0,
                limit_page = 0
            });
        }
        public object queryNonPaginationJSON(string tableORview_name, string sortByColumn, string sort_type)
        {
            checkSort_type(sort_type);
            var request = GetParamsRequest();
            return queryPaginationAndFilterYourWhereJSONnonPagination(new Parame
            {
                table_name = tableORview_name,
                search_text = request.filter,
                page = 0,
                limit_page = 0,
                sortby = sortByColumn,
                sort_type = sort_type
            });
        }
        public object queryNonPaginationJSON(string tableORview_name, ConditionString where, string sortByColumn, string sort_type)
        {
            checkSort_type(sort_type);
            var request = GetParamsRequest();
            return queryPaginationAndFilterYourWhereJSONnonPagination(new Parame
            {
                table_name = tableORview_name,
                yourwhere = where.EXEC,
                key_where = where.EXEC_KEY,
                search_text = request.filter,
                page = 0,
                limit_page = 0,
                sortby = sortByColumn,
                sort_type = sort_type
            });
        }


        //function private
        //normal
        private Pagination<T> queryPaginationAndFilterYourWhere<T>(Parame input)
        {
            try
            {
                Pagination<T> pagination = new Pagination<T>();
                pagination.items = queryPaginationYourWere<T>(input);
                pagination.currentPage = input.page;
                pagination.limitRow = input.limit_page;
                int count = queryPaginationCountRowYourWere(input);
                pagination.resultRow = count;
                return pagination;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private IEnumerable<T> queryPaginationYourWere<T>(Parame input)
        {
            IEnumerable<T> items = db.Database.SqlQuery<T>("EXEC [dbo].[pagination] @table_name,@search_text,@yourwhere,@key_where,@page,@limit_page,@sortby,@sort_type",
                new SqlParameter("@table_name", input.table_name),
                new SqlParameter("@search_text", input.search_text),
                new SqlParameter("@yourwhere", input.yourwhere),
                new SqlParameter("@key_where", input.key_where),
                new SqlParameter("@page", input.page),
                new SqlParameter("@limit_page", input.limit_page),
                new SqlParameter("@sortby", input.sortby),
                new SqlParameter("@sort_type", input.sort_type)
                ).ToList();
            return items;
        }
        //json
        private PaginationJSON queryPaginationAndFilterYourWhereJSON(Parame input)
        {
            try
            {
                PaginationJSON pagination = new PaginationJSON();
                pagination.items = queryPaginationYourWereJSON(input);
                pagination.currentPage = input.page;
                pagination.limitRow = input.limit_page;
                int count = queryPaginationCountRowYourWere(input);
                pagination.resultRow = count;
                return pagination;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private object queryPaginationAndFilterYourWhereJSONnonPagination(Parame input)
        {
            try
            {
                return queryPaginationYourWereJSON(input);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private object queryPaginationYourWereJSON(Parame input)
        {
            string items = db.Database.SqlQuery<string>("EXEC [dbo].[pagination_json] @table_name,@search_text,@yourwhere,@key_where,@page,@limit_page,@sortby,@sort_type",
                new SqlParameter("@table_name", input.table_name),
                new SqlParameter("@search_text", input.search_text),
                new SqlParameter("@yourwhere", input.yourwhere),
                new SqlParameter("@key_where", input.key_where),
                new SqlParameter("@page", input.page),
                new SqlParameter("@limit_page", input.limit_page),
                new SqlParameter("@sortby", input.sortby),
                new SqlParameter("@sort_type", input.sort_type)
                ).FirstOrDefault();
            if (items != null) {
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                var result = json_serializer.DeserializeObject(items);
                return result;
            }
            return new object[]{ };
        }
        private int queryPaginationCountRowYourWere(Parame input)
        {
            int count = db.Database.SqlQuery<int>("EXEC [dbo].[pagination_row_count] @table_name,@search_text,@yourwhere,@key_where",
                    new SqlParameter("@table_name", input.table_name),
                    new SqlParameter("@search_text", input.search_text),
                    new SqlParameter("@yourwhere", input.yourwhere),
                    new SqlParameter("@key_where", input.key_where)
                ).FirstOrDefault();
            return count;
        }
        private void checkSort_type(string sort_type)
        {
            if (sort_type.ToUpper().Equals("ASC") || sort_type.ToUpper().Equals("DESC"))
            {
                return;
            }
            throw new Exception("type sort is incorrent");
        }
        private PaginationParamsRequest GetParamsRequest() {
            HttpRequest Request = HttpContext.Current.Request;
            int ARF_PAGE = string.IsNullOrEmpty(Request["page"]) ? 1 : int.Parse(Request["page"]);
            int ARF_LIMIT = string.IsNullOrEmpty(Request["limit"]) ? 15 : int.Parse(Request["limit"]);
            string ARF_FILTER = string.IsNullOrEmpty(Request["filter"]) ? "" : Request["filter"];
            return new PaginationParamsRequest
            {
                filter = ARF_FILTER,
                limitRow = ARF_LIMIT,
                startRow = ARF_PAGE
            };
        }
    }
    public class PaginationParamsRequest
    {
        public int startRow { get; set; }
        public int limitRow { get; set; }
        public string filter { get; set; }
    }
    public class Pagination<T>
    {
        public IEnumerable<T> items { get; set; }
        public int currentPage { get; set; }
        public int limitRow { get; set; }
        public int resultRow { get; set; }
    }
    public class PaginationJSON
    {
        public object items { get; set; }
        public int currentPage { get; set; }
        public int limitRow { get; set; }
        public int resultRow { get; set; }
    }
    public class Parame
    {
        public string table_name { get; set; } = "";
        public string search_text { get; set; } = "";
        public string yourwhere { get; set; } = "";
        public string key_where { get; set; } = "";
        public int page { get; set; } = 1;
        public int limit_page { get; set; } = 15;
        public string sortby { get; set; } = "";
        public string sort_type { get; set; } = "";
    }
}
