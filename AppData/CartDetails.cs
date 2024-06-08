using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppData
{
    public class CartDetails
    {
        [Key]
        public Guid CartDetailsID { get; set; }
        public string? BookID { get; set; }
        [ForeignKey("Username")]
        public string Username { get; set; } = null!;
        public int Quantity { get; set; }
        public int Status { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual Book? Book { get; set; }
    }
}
