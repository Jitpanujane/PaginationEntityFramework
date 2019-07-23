namespace Pagination.MultipleResultStoreProc.ModelProc
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class TableA
    {
        [Key]
        public int Id { get; set; }
        public int a1 { get; set; }
        public int a2 { get; set; }
        public int a3 { get; set; }
    }
}
