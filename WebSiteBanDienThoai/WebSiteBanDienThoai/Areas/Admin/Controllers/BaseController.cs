using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebSiteBanDienThoai.Common.Utils;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
         // GET: /Administrator/Base/
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = (User)Session[SessionKey.User];

            if (user != null)
            {
                if (user.RoleId != RoleKey.Admin)
                {

                    filterContext.Result =
                        new RedirectToRouteResult(
                            new RouteValueDictionary(new { controller = "Error", action = "E401" , area = "" }));
                }

            }
            else
            {

                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Account", action = "Login" , area = "" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}