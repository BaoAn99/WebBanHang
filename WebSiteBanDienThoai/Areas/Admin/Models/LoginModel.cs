using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
        public string Role { get; set; }


       
    }
}
   