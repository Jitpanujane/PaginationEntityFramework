using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.Pagination.Installer
{
    public static class Starter
    {
        public static void go(DbContext db)
        {
            //Check have s_Pagination
            string script = EXEC_installer.installerSQL;
            var check_have = db.Database.SqlQuery<installer>(script).FirstOrDefault();
            if (check_have.count_store == 0)
            {
                //Install s_Pagination
                script = EXEC_s_Pagination.s_PaginationSQL;
                int create = db.Database.ExecuteSqlCommand(script);
                if (create == 0)
                {
                    throw new Exception("Filed for create store EXEC_s_Pagination.sql");
                }
            }
            //Check have s_PaginationJSON
            script = EXEC_installer.installerSQLJSON;
            check_have = db.Database.SqlQuery<installer>(script).FirstOrDefault();
            if (check_have.count_store == 0)
            {
                //Install s_PaginationJSON
                script = EXEC_s_Pagination.s_PaginationSQLJSON;
                int create = db.Database.ExecuteSqlCommand(script);
                if (create == 0)
                {
                    throw new Exception("Filed for create store EXEC_s_PaginationJSON.sql");
                }
            }
        }
        public class installer
        {
            public int Id { get; set; }
            public int count_store { get; set; }
        }
    }
}
