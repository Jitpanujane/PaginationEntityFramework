using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.Pagination.Installer
{
    public static class EXEC_s_Pagination
    {
        public static string s_PaginationSQL { get; } = @"
           CREATE PROCEDURE [dbo].[s_Pagination]
                @TableOrView NVARCHAR(50),
                @Where NVARCHAR(MAX) = '',
                @FilterColumn NVARCHAR(50) = '',
                @FilterValue NVARCHAR(MAX) = '',
                @Page INT = 0,
                @Limit_page INT = 0,
                @Sortby NVARCHAR(MAX) = ''
            AS
            BEGIN
                SET NOCOUNT ON;

                --Return
                declare @table table(
                    Id INT IDENTITY(1,1) NOT NULL,
                    Count_row int
                )
                DECLARE @count INT;

                --Process
                DECLARE @SQL NVARCHAR(MAX);
                DECLARE @SQL_COUNT NVARCHAR(MAX);
                DECLARE @SQL_CHECK_TYPE INT;
                DECLARE @SQL_FILTER NVARCHAR(MAX) = '';
                DECLARE @SQL_ORDERBY NVARCHAR(MAX);

                --Auto filter pagination
                IF(@FilterValue != '' AND @FilterColumn != '') 
                BEGIN
                    --[system_type_id] in (35,99,167,175,231,239) is string
                    --[system_type_id] in (40,41,42,43,58,61) is date

                    SELECT @SQL_CHECK_TYPE = ty.system_type_id
                    FROM sys.columns cl
                        JOIN sys.types ty on ty.user_type_id = cl.user_type_id
                    WHERE cl.object_id = OBJECT_ID(@TableOrView) AND UPPER(cl.name) = UPPER(@FilterColumn)

                    IF(@SQL_CHECK_TYPE IN (35,99,167,175,231,239)) --is string
                    BEGIN
                        SET @SQL_FILTER = @FilterColumn+' LIKE '+'''%'+@FilterValue+'%'''
                    END
                    ELSE IF(@SQL_CHECK_TYPE IN (40,41,42,43,58,61)) --is date
                    BEGIN
                        SET @SQL_FILTER = 'CONVERT(DATE,'+@FilterColumn+')'+' = '+'CAST('''+@FilterValue+''' as DATE)'
                    END
                    ELSE -- is int
                    BEGIN
                        SET @SQL_FILTER = @FilterColumn+' = '+@FilterValue
                    END

                    SET @SQL_FILTER = 'WHERE( '+@SQL_FILTER+' )'

                    PRINT CONCAT('Auto filter is', @SQL_FILTER);
                END
                --Auto pagination and sort
                IF(@Page != 0 AND @Limit_page != 0)
                BEGIN
                    DECLARE @SQL_OFFSET NVARCHAR(MAX) = ((@Page-1)* @Limit_page);
                    DECLARE @SQL_MAX NVARCHAR(MAX) = @Limit_page;
                    IF(@Sortby = '')
                    BEGIN
                        SELECT TOP 1
                            @Sortby = cl.[name]
                        FROM sys.columns cl
                        WHERE cl.object_id = OBJECT_ID(@TableOrView)
                        SET @SQL_ORDERBY = ' ORDER BY '+ @Sortby +' ASC OFFSET ' + @SQL_OFFSET + ' ROWS FETCH NEXT ' + @SQL_MAX + ' ROWS ONLY'
                    END
                    ELSE
                    BEGIN
                        SET @SQL_ORDERBY = ' ORDER BY ' + @Sortby +' OFFSET ' + @SQL_OFFSET + ' ROWS FETCH NEXT ' + @SQL_MAX + ' ROWS ONLY'
                    END
                    SET @SQL = (N'WITH CTE AS (SELECT * FROM '+@TableOrView+' '+@Where+') SELECT * FROM CTE '+@SQL_FILTER+' '+@SQL_ORDERBY);
                END
                ELSE
                BEGIN
                    SET @SQL = (N'WITH CTE AS (SELECT * FROM '+@TableOrView+' '+@Where+') SELECT * FROM CTE '+@SQL_FILTER);
                END

                SET @SQL_COUNT = ('WITH CTE AS (SELECT * FROM ' + @TableOrView + ' ' + @Where + ') SELECT @count=COUNT(*) FROM CTE ' + @SQL_FILTER);

                --Execute and return
                PRINT @SQL;
                EXEC sp_executesql @SQL;

                EXEC sp_executesql @SQL_COUNT,N'@count NVARCHAR(MAX) output', @count output;

                insert @table values(@count)

                SELECT*
                FROM @table;
            END
        ";
        public static string s_PaginationSQLJSON { get; } = @"
            CREATE PROCEDURE [dbo].[s_PaginationJSON]
                @TableOrView NVARCHAR(50),
                @Where NVARCHAR(MAX) = '',
                @FilterColumn NVARCHAR(50) = '',
                @FilterValue NVARCHAR(MAX) = '',
                @Page INT = 0,
                @Limit_page INT = 0,
                @Sortby NVARCHAR(MAX) = ''
            AS
            BEGIN
                SET NOCOUNT ON;

                --Return
                declare @table table(
                    Id INT IDENTITY(1,1) NOT NULL,
                    Item nvarchar(MAX),
                    Count_row int
                )
                DECLARE @item NVARCHAR(MAX);
                DECLARE @count INT;

                --Process
                DECLARE @SQL NVARCHAR(MAX);
                DECLARE @SQL_COUNT NVARCHAR(MAX);
                DECLARE @SQL_CHECK_TYPE INT;
                DECLARE @SQL_FILTER NVARCHAR(MAX) = '';
                DECLARE @SQL_ORDERBY NVARCHAR(MAX);

                --Auto filter pagination
                IF(@FilterValue != '' AND @FilterColumn != '') 
                BEGIN
                    --[system_type_id] in (35,99,167,175,231,239) is string
                    --[system_type_id] in (40,41,42,43,58,61) is date

                    SELECT @SQL_CHECK_TYPE = ty.system_type_id
                    FROM sys.columns cl
                        JOIN sys.types ty on ty.user_type_id = cl.user_type_id
                    WHERE cl.object_id = OBJECT_ID(@TableOrView) AND UPPER(cl.name) = UPPER(@FilterColumn)

                    IF(@SQL_CHECK_TYPE IN (35,99,167,175,231,239)) --is string
                    BEGIN
                        SET @SQL_FILTER = @FilterColumn+' LIKE '+'''%'+@FilterValue+'%'''
                    END
                    ELSE IF(@SQL_CHECK_TYPE IN (40,41,42,43,58,61)) --is date
                    BEGIN
                        SET @SQL_FILTER = 'CONVERT(DATE,'+@FilterColumn+')'+' = '+'CAST('''+@FilterValue+''' as DATE)'
                    END
                    ELSE -- is int
                    BEGIN
                        SET @SQL_FILTER = @FilterColumn+' = '+@FilterValue
                    END
                    SET @SQL_FILTER = 'WHERE( '+@SQL_FILTER+' )'
                    PRINT CONCAT('Auto filter is', @SQL_FILTER);
                END
                --Auto pagination and sort
				IF(@Sortby = '')
                    BEGIN
                        SELECT TOP 1
                            @Sortby = cl.[name]
                        FROM sys.columns cl
                        WHERE cl.object_id = OBJECT_ID(@TableOrView)
                        SET @SQL_ORDERBY = ' ORDER BY '+ @Sortby +' ASC';
                    END
                    ELSE
                    BEGIN
                        SET @SQL_ORDERBY = ' ORDER BY ' + @Sortby;
                    END
                IF(@Page != 0 AND @Limit_page != 0)
                BEGIN
                    DECLARE @SQL_OFFSET NVARCHAR(MAX) = ((@Page-1)* @Limit_page);
                    DECLARE @SQL_MAX NVARCHAR(MAX) = @Limit_page;
					SET @SQL_ORDERBY = @SQL_ORDERBY + ' OFFSET ' + @SQL_OFFSET + ' ROWS FETCH NEXT ' + @SQL_MAX + ' ROWS ONLY';
                    SET @SQL = (N'WITH CTE AS (SELECT * FROM '+@TableOrView+' '+@Where+') SELECT @item = (SELECT * FROM CTE '+@SQL_FILTER+' '+@SQL_ORDERBY+' FOR JSON AUTO, INCLUDE_NULL_VALUES)');
                END
                ELSE
                BEGIN
                    SET @SQL = (N'WITH CTE AS (SELECT * FROM '+@TableOrView+' '+@Where+') SELECT @item = (SELECT * FROM CTE '+@SQL_FILTER+' '+@SQL_ORDERBY+' FOR JSON AUTO, INCLUDE_NULL_VALUES)');
                END

                SET @SQL_COUNT = ('WITH CTE AS (SELECT * FROM ' + @TableOrView + ' ' + @Where + ') SELECT @count=COUNT(*) FROM CTE ' + @SQL_FILTER);

                --Execute and return
                exec sp_executesql @SQL,N'@item NVARCHAR(MAX) output', @item output;

                exec sp_executesql @SQL_COUNT,N'@count NVARCHAR(MAX) output', @count output;

                insert @table
                values(@item, @count)

                SELECT*
                FROM @table;
            END
        ";
    }
}
