using System.ComponentModel.DataAnnotations;

namespace WebTruyenChu_210BANED.Models
{
    public class userModel
    {
        [Required(ErrorMessage = "Tên người dùng không được để trống")]
        public string ? Username { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string ? Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string? Password { get; set; }
    }

}
