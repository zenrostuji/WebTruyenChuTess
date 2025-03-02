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

    public class FollowedStory
    {
        public int Id { get; set; }
        public string UserId { get; set; } // ID của user theo dõi truyện
        public int StoryId { get; set; } // ID của truyện được theo dõi
        public StoryCard Story { get; set; } // Liên kết đến bảng Story
    }

}
