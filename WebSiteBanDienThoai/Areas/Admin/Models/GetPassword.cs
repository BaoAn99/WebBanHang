using System.ComponentModel.DataAnnotations;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class GetPassword
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "vui lòng nhập đúng đại chỉ email đã đăng ký")]
        [EmailAddress(ErrorMessage = "vui lòng nhập đúng đại chỉ email đã đăng ký")]
        public string Email { get; set; }
    }
}