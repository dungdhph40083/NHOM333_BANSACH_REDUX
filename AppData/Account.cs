using System.ComponentModel.DataAnnotations;

namespace AppData
{
    public class Account
    {
        [Key]
        [Required]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Tên người dùng phải từ 6 - 256 ký tự.")]
        public string Username { get; set; } = null!;
        [Required]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 - 256 ký tự.")]
        public string? Password { get; set; }
        [EmailAddress(ErrorMessage = "Định dạng phải đúng quy tắc, ví dụ như: vidu@hotmail.com")]
        public string? Email { get; set; }
        [RegularExpression("\\d{10,16}")]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int Status { get; set; }
        public virtual List<Bill>? Bills { get; set; }
        public virtual Cart? Cart { get; set; }
    }
}
