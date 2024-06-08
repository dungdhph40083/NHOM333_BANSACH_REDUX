using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppData
{
    public class Cart
    {
        [Key]
        [ForeignKey("Account")]
        public string Username { get; set; } = null!;
        public int Status { get; set; }
        public Account? Account { get; set; }
    }
}
