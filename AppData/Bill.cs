using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppData
{
    public class Bill
    {
        [Key]
        public Guid BillID { get; set; }
        public string? Description { get; set; }
        public DateTime CreationTime { get; set; }
        [ForeignKey("Account")]
        public string? Username { get; set; }
        public int Status { get; set; }
        public virtual Account? Account { get; set; }
        public virtual List<BillDetails>? BillDetails { get; set; }
    }
}
