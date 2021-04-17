using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using WebSiteBanDienThoai.Entity;
using System.Threading.Tasks;
using WebSiteBanDienThoai.Core.Entity;
using PagedList;
using PagedList.Mvc;
namespace WebSiteBanDienThoai.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var unitOfWork = new UnitOfWork(new QLBHDienThoaiEntities());
           
            var products = unitOfWork.Product.GetDataHome();
            return View(products);
        }
    }
}