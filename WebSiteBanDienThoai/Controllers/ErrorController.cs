using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteBanDienThoai.Controllers
{
    public class ErrorController : Controller
    {
        
        public ActionResult E401()
        {
            return View();
        }
    }
}