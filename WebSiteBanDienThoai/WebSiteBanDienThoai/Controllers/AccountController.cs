using System.Web.Mvc;
using WebSiteBanDienThoai.Common.Utils;
using WebSiteBanDienThoai.Core.Entity;
using WebSiteBanDienThoai.Core.Utils;
using WebSiteBanDienThoai.Entity;
using WebSiteBanDienThoai.Model;

namespace WebSiteBanDienThoai.Controllers
{
    public class AccountController : BaseController
    {

        public ActionResult Index()
        {
            if (IsExistSession(SessionKey.User))
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public ActionResult Login()
        {
            if (IsExistSession(SessionKey.User))
            {
                return Redirect("/");
            }
            return View();
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public JsonResult CheckLogin(string username, string password)
        {
            if (IsExistSession(SessionKey.User))
            {
                return Json(new { status = true, mess = "Đã đăng nhập" });
            }
            var unitOfWork = new UnitOfWork(new QLBHDienThoaiEntities());
            int status = unitOfWork.Account.CheckAccount(username, password);
            switch (status)
            {
                case -2:
                    return Json(new { status = false, mess = "Tài khoản bạn đã bị khóa do hủy quá 3 đơn hàng" });
                case -1:
                    return Json(new { status = false, mess = "Tên và mật khẩu không chính xác" });
                case 0:
                    return Json(new { status = false, mess = "Tài khoản chưa xác thực" });
                case 1:
                    {
                        var account = unitOfWork.Account.GetAccountByUsername(username, password);
                        if (account.RoleId == RoleKey.Employee || account.RoleId == RoleKey.Admin)
                        {
                            return Json(new { status = false, mess = "Đăng nhập thất bại" });
                        }
                        if (account != null)
                        {
                            Session[SessionKey.User] = account;
                            return Json(new { status = true, mess = "Đăng nhập thành công" });
                        }
                        else
                        {
                            return Json(new { status = false, mess = "Đăng nhập thất bại" });
                        }
                    }
                default:
                    return Json(new { status = false, mess = "Đăng nhập thất bại" });
            }
        }

        public ActionResult Register()
        {
            if (IsExistSession(SessionKey.User))
            {
                return Redirect("/");
            }
            ResponseRegister rp = new ResponseRegister();
            rp.Acc = new User();
            rp.Status = true;
            return View(rp);
        }

        [HttpPost]
        public ActionResult CreateAccount(User ac)
        {
            ResponseRegister rp = new ResponseRegister();

            var unitOfWork = new UnitOfWork(new QLBHDienThoaiEntities());
            var validAccount = unitOfWork.Account.ValidAccount(ac);
            switch (validAccount)
            {
                case -1:
                    {
                        rp.Acc = ac;
                        rp.Status = false;
                        rp.Message = "Email đã tồn tại";
                        return View("Register", rp);
                    }
                case 0:
                    {
                        rp.Acc = ac;
                        rp.Status = false;
                        rp.Message = "Username đã tồn tại";
                        return View("Register", rp);
                    }
            }
            var response = AccountUtils.SendEmail(ac);
            if (response.Status == true)
            {
                ac.Code = response.Code;
                ac.IsLocked = true;
                ac.Status = false;
                ac.EmailConfirmed = false;
                unitOfWork.Account.Add(ac);
                unitOfWork.Complete();
                Session[SessionKey.RegUser] =
                    unitOfWork.Account.GetAccountByUsername(ac.Username, ac.Password);
                return View("ConfirmEmail");
            }
            else
            {
                return RedirectToAction("Register");
            }
        }
        public ActionResult ConfirmEmail()
        {
            if (IsExistSession(SessionKey.RegUser))
            {
                var acc = (User)Session[SessionKey.RegUser];
                return View(acc.Email);
            }
            return View("");
        }

        public JsonResult Confirm(string email, string code)
        {
            var unitOfWork = new UnitOfWork(new QLBHDienThoaiEntities());
            var userReg = (User)Session[SessionKey.RegUser];
            if (userReg == null)
            {
                var userEmail = unitOfWork.Account.GetAccountByEmail(email);
                if (userEmail == null)
                {
                    return Json(new { status = false, mess = "Không tồn tại email này" });
                }
                userReg = userEmail;
            }
            var isValid = unitOfWork.Account.ConfirmCode(userReg, code);
            if (isValid)
            {
                Session[SessionKey.User] = unitOfWork.Account.GetAccountByEmail(userReg.Email);
                return Json(new { status = true, mess = "Xác thực thành công" });
            }
            else
            {
                return Json(new { status = false, mess = "Mã xác thực không đúng" });
            }
        }

        public JsonResult ChangePass(string oldPass, string newPass, string reNewPass)
        {
            if (Session[SessionKey.User] != null)
            {
                User user = (User)Session[SessionKey.User];
                if (!user.Password.Equals(oldPass))
                {
                    return Json(new { status = false, mess = "Mật khẩu cũ không khớp!" });
                }
                if (!newPass.Equals(reNewPass))
                {
                    return Json(new { status = false, mess = "Mật khẩu không khớp!" });
                }
                var unitOfWork = new UnitOfWork(new QLBHDienThoaiEntities());
                var us = unitOfWork.Account.GetAccountByUsername(user.Username, user.Password);
                us.Password = newPass;
                unitOfWork.Complete();
                return Json(new { status = true, mess = "Đổi mật khẩu thành công!", url = "/Account/Logout" });
            }
            return Json(new { status = "login", mess = "Đăng nhập lại!", url = "/Account/Login" });
        }

        public ActionResult Forgot()
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Forgot(string passnew)
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Recover(string email)
        {
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session[SessionKey.User] = null;
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.ToString());
            else
            {
                return Redirect("/");
            }
        }

        public bool IsExistSession(string name)
        {
            if ((User)Session[name] != null)
            {
                return true;
            }
            return false;
        }
    }
}