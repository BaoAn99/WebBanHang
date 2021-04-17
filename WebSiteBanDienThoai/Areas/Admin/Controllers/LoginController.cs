using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanDienThoai.Common.Utils;
using WebSiteBanDienThoai.Core.Entity;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Administrator/Login/

        [HttpGet]
        public ActionResult Index()
        {
            
            if (Session[SessionKey.Admin] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CheckLogin(string userName, string passWord)
        {
            var unitofwork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var acc = unitofwork.Account.GetAccountByUsername(userName, passWord);
            if (acc != null && (acc.RoleId == 1 || acc.RoleId == 2))
            {
                Session[SessionKey.Admin] = acc;
                Session.Timeout = 60;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public JsonResult ChangePass(string oldPass, string newPass, string reNewPass)
        {
            if (Session[SessionKey.Admin] != null)
            {
                User user = (User)Session[SessionKey.Admin];
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
                return Json(new { status = true, mess = "Đổi mật khẩu thành công!", url = "/Login/Logout" });
            }
            return Json(new { status = "login", mess = "Đăng nhập lại!", url = "/Login/Index" });
        }
        //[HttpPost]
        //[ValidateInput(true)]
        //public ActionResult Index(LoginModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        int checklogin = CheckLogin.CheckUserLogin(model.UserName, model.Password);
        //        switch (checklogin)
        //        {
        //            case 1:

        //                //Đăng nhập thành công
        //                string cookieClient =  model.UserName + "||";
        //                string decodeCookieClient = CryptorEngine.Encrypt(cookieClient, true);
        //                HttpCookie userCookie = new HttpCookie("name_client");
        //                userCookie.Value = decodeCookieClient;
        //                userCookie.Expires = DateTime.Now.AddDays(30);
        //                HttpContext.Response.Cookies.Add(userCookie);
        //                CurrentSession.Logined = true;

        //                TempData["Count"] = 0;
        //                TempData["Messages"] = "Đăng nhập thành công";
        //                return RedirectToAction("Index", "Dashboard");
        //            case 2:
        //                TempData["Messages"] = "Tài khoản đã bị khóa";
        //                return RedirectToAction("Index");
        //            case 3:
        //                if (TempData["Count"] == null)
        //                {
        //                    TempData["Count"] = 1;
        //                    TempData.Keep("Count");
        //                }
        //                else
        //                {
        //                    TempData["Count"] = int.Parse(TempData["Count"].ToString()) + 1;
        //                    TempData.Keep("Count");
        //                }


        //                ViewBag.Messages = "Tên đăng nhập và mật khẩu không đúng";
        //                return View(model);
        //        }
        //    }
        //    return View();
        //}

        //[HttpGet]
        //[ValidateInput(true)]
        //public ActionResult GetPassWord()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateInput(true)]
        //public ActionResult GetPassWord(GetPassword model)
        //{
        //    using (var db = new MrLongToeicEntities())
        //    {
        //        User detailUser =
        //            db.Users.FirstOrDefault(a => a.Email == model.Email && a.UserName == model.UserName);
        //        if (Convert.ToDateTime(CurrentSession.LockUser) > DateTime.Now)
        //        {
        //            DateTime dateBlog = Convert.ToDateTime(CurrentSession.LockUser);
        //            int minuteLock = dateBlog.Minute + (dateBlog.Hour*60) - DateTime.Now.Minute - (DateTime.Now.Hour*60);
        //            ModelState.AddModelError("Email",
        //                "Bạn đã nhập quá nhiều lần quy định, xin vui lòng quay lại sau " + minuteLock + " Phút.");
        //            return View();
        //        }
        //        if (detailUser == null)
        //        {
        //            if (TempData["Count"] == null)
        //            {
        //                TempData["Count"] = 1;
        //                TempData.Keep("Count");
        //            }
        //            else
        //            {
        //                TempData["Count"] = int.Parse(TempData["Count"].ToString()) + 1;
        //                TempData.Keep("Count");
        //            }
        //            if (int.Parse(TempData["Count"].ToString()) == 5)
        //            {
        //                DateTime dateBlog = DateTime.Now.AddMinutes(1);
        //                CurrentSession.LockUser = dateBlog;
        //                int minuteLock = dateBlog.Minute + (dateBlog.Hour*60) - DateTime.Now.Minute -
        //                                 (DateTime.Now.Hour*60);
        //                TempData.Remove("Count");
        //                ModelState.AddModelError("Email",
        //                    "Bạn đã nhập quá nhiều lần quy định, xin vui lòng quay lại sau " + minuteLock + " phút.");
        //                return View();
        //            }
        //            ModelState.AddModelError("Email",
        //                "Email hoặc tên người dùng là không chính xác, bạn còn " +
        //                (5 - int.Parse(TempData["Count"].ToString())) + " lần nhập");

        //            return View();
        //        }
        //        string content =
        //            System.IO.File.ReadAllText(Server.MapPath("/Areas/Admin/lib/Forgot_Password.html"));

        //        content = content.Replace("{{Password}}", CryptorEngine.Decrypt(detailUser.PasswordOld, true));

        //        MailHelper.SendMail(detailUser.Email, "Lấy lại mật khẩu", content);


        //        ViewBag.Messeages = "Vui lòng đăng nhập vào địa chỉ email: " + model.Email + " để lấy lại mật khẩu.";
        //        return View();
        //    }
        //}

        //[HttpGet]
        //public ActionResult Logout()
        //{
        //    int cout = 0;
        //    HttpCookie langCookie = Request.Cookies["lang_client"];
        //    while (langCookie !=null)
        //    {
        //        langCookie.Expires = DateTime.Now.AddDays(-30);
        //        HttpContext.Response.Cookies.Add(langCookie);
        //        cout++;
        //        if(cout == 10)
        //            break;
        //    }
        //    cout = 0;
        //    HttpCookie nameCookie = Request.Cookies["name_client"];
        //    while (nameCookie !=null)
        //    {
        //        nameCookie.Expires = DateTime.Now.AddDays(-30);
        //        HttpContext.Response.Cookies.Add(nameCookie);
        //        cout++;
        //        if(cout == 10)
        //            break;
        //    }
        //   // CurrentSession.ClearAll();
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public ActionResult Logout()
        {
            if (Session[SessionKey.Admin] != null)
            {
                Session[SessionKey.Admin] = null;
            }
            return RedirectToAction("Index","Login");
        }
    }
}