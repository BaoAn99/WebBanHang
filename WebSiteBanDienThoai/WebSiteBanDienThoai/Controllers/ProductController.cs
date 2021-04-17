using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebSiteBanDienThoai.Common.Utils;
using WebSiteBanDienThoai.Core.Entity;
using WebSiteBanDienThoai.Entity;
using WebSiteBanDienThoai.Model;

namespace WebSiteBanDienThoai.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
           
            var unitofwork = new UnitOfWork(new QLBHDienThoaiEntities());
            List<Product> lstProduct = unitofwork.Product.GetDataHome();
            return View(lstProduct);
        }
        
       
        public ActionResult Detail(int ProductID)
        {
            var unitofwork = new UnitOfWork(new QLBHDienThoaiEntities());
            var product = unitofwork.Product.Detial(ProductID);
            var promotionList = unitofwork.Promotion.GetAll();
            Promotion promotion = new Promotion();
            promotion.SaleOff = 0;
            foreach(var item in promotionList)
            {
                if (item.ProductID != null)
                {                    
                    if(item.ProductID == product.ProductID&&promotion.SaleOff<item.SaleOff&&item.EndTime>DateTime.Now)
                    {
                        promotion = (item);
                    }
                }
                else if (item.TypeProductID != 0)
                {
                    if (item.TypeProductID == product.TypeProductID&& promotion.SaleOff < item.SaleOff&&item.EndTime > DateTime.Now)
                    {
                        promotion = (item);
                    }
                }
            }
            ViewBag.promotion = promotion;
            if(Session[SessionKey.User]!=null)
            {
                var id = ((User)Session[SessionKey.User]).CustormerId;
                ViewBag.UserId = id == null ? -1 : id;
            }
            else
            {
                ViewBag.UserId = 0;
            }                
            return View(product);
        }

        public ActionResult Search(string key = "") { 
        
            try{

                var unitOfWork = new UnitOfWork(new QLBHDienThoaiEntities());
                Expression<Func<Product, object>>[] includes = new Expression<Func<Product, object>>[1];
                includes[0] = x => x.TypeProduct;
                var listData = unitOfWork.Product.GetAll();
                foreach (var item in listData)
                {
                    item.TypeProduct = unitOfWork.TypeProduct.Get(item.TypeProductID.GetValueOrDefault());
                }
                var result = listData.Where(x => x.Name.ToLower().Contains(key.Trim().ToLower()) || x.TypeProduct.TypeProductName.ToLower().Contains(key.Trim().ToLower())).ToList();
                ViewBag.key = key;
                return View(result);
            }
            catch (Exception ex) { return View(new List<Product>()); };
            
        }


        public ActionResult Category(int TypeProductID)
        {
            var unitofwork = new UnitOfWork(new QLBHDienThoaiEntities());
            List<Product> lstProduct = unitofwork.Product.Category(TypeProductID);
            return View(lstProduct);
        }
        #region Cart
        public ContentResult AddToCart(Product product)
        {
            List<Product> ProductList = new List<Product>();
            var unitofwork = new UnitOfWork(new QLBHDienThoaiEntities());
            var pro = unitofwork.Product.Get(product.ProductID);
            
            int amountInDb = pro.Amount.GetValueOrDefault();
            pro.Amount = product.Amount;
            try { 
                
                if (Session["Cart"]!=null)
                {
                     ProductList= (List<Product>)Session["Cart"];
                    if (!pro.Status.GetValueOrDefault())
                    {
                        return Content(ResponseData.ToJson(new ResponseData(ProductList, 0, false, null, "<p></p>Không thể thêm! Sản phẩm " + pro.Name + " đã ngừng kinh doanh!")));
                    }
                    if (amountInDb < product.Amount)
                    {
                        return Content(ResponseData.ToJson(new ResponseData(ProductList, 0, false, null, "<p></p>Không thể thêm! Số lượng " + pro.Name + " chỉ còn "+amountInDb.ToString()+" sản phẩm!")));
                    }
                    if (!ProductList.Any(x=>x.ProductID==pro.ProductID))
                        ProductList.Add(pro);
                    else
                        return Content(ResponseData.ToJson(new ResponseData(ProductList, 0, false, null,"<p></p>Không thể thêm! "+ pro.Name + " đã có trong giỏ hàng!")));
                }
                else{
                    if (!pro.Status.GetValueOrDefault())
                    {
                        return Content(ResponseData.ToJson(new ResponseData(ProductList, 0, false, null, "<p></p>Không thể thêm! Sản phẩm " + pro.Name + " đã ngừng kinh doanh!")));
                    }
                    if (amountInDb < product.Amount)
                    {
                        return Content(ResponseData.ToJson(new ResponseData(ProductList, 0, false, null, "<p></p>Không thể thêm! Số lượng " + pro.Name + " chỉ còn " + amountInDb.ToString() + " sản phẩm!")));
                    }
                    ProductList.Add(pro);
                }
                Session["Cart"] = ProductList;
                return Content(ResponseData.ToJson(new ResponseData(ProductList,ProductList.Sum(x=>x.Amount*x.PriceProduct).GetValueOrDefault(), true, null, "<p></p>" + pro.Name + " được thêm vào giỏ hàng thành công!")));
            }catch(Exception ex)
            {
                return Content(ResponseData.ToJson(new ResponseData(ProductList,0, true, null,"<p></p>"+ pro.Name + " thêm vào giỏ hàng thất bại!")));
            }
        }
        public ContentResult DeleteToCart(Product product)
        {
            List<Product> ProductList = new List<Product>();
            if (Session["Cart"] != null)
            {
                ProductList = (List<Product>)Session["Cart"];
                product = ProductList.Single(x => x.ProductID == product.ProductID);
                ProductList.Remove(product);
            }
            Session["Cart"] = ProductList;
            return Content(ResponseData.ToJson(new ResponseData(ProductList, ProductList.Sum(x => x.Amount * x.PriceProduct).GetValueOrDefault(), true, null, "<p></p>" + product.Name + " đã được xóa khỏi giỏ hàng!")));
        }
        public ContentResult EditToCart(Product product)
        {
            List<Product> ProductList = new List<Product>();
            var unitofwork = new UnitOfWork(new QLBHDienThoaiEntities());            
            int amountInDb = unitofwork.Product.Get(product.ProductID).Amount.GetValueOrDefault();
            if (Session["Cart"] != null)
            {
                ProductList = (List<Product>)Session["Cart"];
                if (amountInDb < product.Amount)
                {
                    return Content(ResponseData.ToJson(new ResponseData(ProductList, 0, false, null, "<p></p>Không thể thêm! Số lượng " + product.Name + " chỉ còn " + amountInDb.ToString() + " sản phẩm!")));
                }
                var pro = ProductList.Single(x => x.ProductID == product.ProductID);
                pro.Amount = product.Amount;
                Session["Cart"] = ProductList;
                return Content(ResponseData.ToJson(new ResponseData(ProductList, ProductList.Sum(x => x.Amount * x.PriceProduct).GetValueOrDefault(), true, null, "<p></p>" + pro.Name + " được sửa vào giỏ hàng thành công!")));
            }
            else
            {
                return Content(ResponseData.ToJson(new ResponseData(ProductList, ProductList.Sum(x => x.Amount * x.PriceProduct).GetValueOrDefault(), true, null, "Giỏ hàng trống!")));
            }            
            
        }
        #endregion Cart
        // public PartialViewResult PartialCategory()
        //{
        //    var unitofwork = new UnitOfWork(new QLBHDienThoaiEntities());
        //    return PartialView(unitofwork.Category.GetData());
        //}
    }
}