using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.Pagination.Installer
{
    public static class EXEC_installer
    {
        public static string installerSQL { get; } = @"select 1 as Id,COUNT(*) as count_store from information_schema.routines where routine_type = 'PROCEDURE' AND ROUTINE_NAME = 's_Pagination'";
        public static string installerSQLJSON { get; } = @"select 1 as Id,COUNT(*) as count_store from information_schema.routines where routine_type = 'PROCEDURE' AND ROUTINE_NAME = 's_PaginationJSON'";
    }
}
