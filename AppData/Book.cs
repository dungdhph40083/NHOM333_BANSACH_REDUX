
using System.ComponentModel.DataAnnotations;

namespace AppData
{
    public class Book
    {
        [Key]
        public string BookID { get; set; } = null!;

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }
        public virtual List<BillDetails>? BillDetails { get; set; }
        public virtual List<CartDetails>? CartDetails { get; set; }
    }
}
