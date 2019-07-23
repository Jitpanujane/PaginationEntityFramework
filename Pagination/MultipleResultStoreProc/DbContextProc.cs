using Pagination.MultipleResultStoreProc.ModelProc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.MultipleResultStoreProc
{
    public partial class DbContextProc : DbContext
    {
        public DbContextProc(string ConnectionString)
          : base(ConnectionString)
        {
        }
        public virtual DbSet<TableA> TableA { get; set; }
        public virtual DbSet<TableB> TableB { get; set; }
        public virtual DbSet<resObject> resObject { get; set; }
    }
    public class resObject
    {
        public int Id { get; set; }
        public int Count_row { get; set; }
    }
}
