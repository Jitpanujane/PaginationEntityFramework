using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagination.MultipleResultStoreProc.ModelProc
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class TableB
    {
        [Key]
        public int Id { get; set; }
        public int b1 { get; set; }
        public int b2 { get; set; }
        public int b3 { get; set; }
    }
}
