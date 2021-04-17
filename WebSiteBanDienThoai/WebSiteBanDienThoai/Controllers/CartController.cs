using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebSiteBanDienThoai.Common.Utils;
using WebSiteBanDienThoai.Core.Entity;
using WebSiteBanDienThoai.Entity;
using WebSiteBanDienThoai.Model;

namespace WebSiteBanDienThoai.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ContentResult getCart()
        {
            if (Session["cart"] == null)
                return Content(ResponseData.ToJson(new ResponseData(null, 0, true, null, null)));
            List<Product> productList = (List<Product>)Session["cart"];
            return Content(ResponseData.ToJson(new ResponseData(productList, 0, true, null, null)));
        }
        public ActionResult CheckOut()
        {
            if (Session["user"] != null)
            {
                var unitOfWork = new UnitOfWork(new QLBHDienThoaiEntities());
                var user = (User)Session["user"];
                if (user.CustormerId != null)
                {

                    Session["customer"] = unitOfWork.Customer.Get(user.CustormerId.GetValueOrDefault());
                }
                else
                    Session["customer"] = new Customer();

                var payments = unitOfWork.Payment.Query(x => x.Status).ToList();
                ViewBag.Payments = new SelectList(payments, "PaymentId", "Name");
                return View();
            }
            Session["isCreateBill"] = "isHave";
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ContentResult CheckPromotion(Product prod, Promotion prom)
        {
            var unitOfWork = new UnitOfWork(new QLBHDienThoaiEntities());
            var promotionList = unitOfWork.Promotion.GetAll();
            prom.SaleOff = 0;
            prom = promotionList.FirstOrDefault(x => x.PromotionName == prom.PromotionName);
            prod = unitOfWork.Product.Get(prod.ProductID);
            if (prom != null)
            {
                if (prom.ProductID != prod.ProductID && prom.TypeProductID != prod.TypeProductID)
                {
                    prom.SaleOff = 0;
                    return Content(ResponseData.ToJson(new ResponseData(prom, 0, false, null, "Mã giảm giá " + prom.PromotionName + " không áp dụng cho sản phẩm này!")));
                }
                if (prom.StartTime <= DateTime.Now && prom.EndTime >= DateTime.Now)
                {
                    return Content(ResponseData.ToJson(new ResponseData(prom, 0, true, null, "Mã giảm giá " + prom.PromotionName + " là chính xác!")));
                }
                prom.SaleOff = 0;
                return Content(ResponseData.ToJson(new ResponseData(prom, 0, false, null, "Mã giảm giá " + prom.PromotionName + " đã hết hạn!")));
            }
            prom = new Promotion();
            prom.SaleOff = 0;
            return Content(ResponseData.ToJson(new ResponseData(prom, 0, false, null, "Mã giảm giá " + prom.PromotionName + " không tồn tại!")));
        }
        [HttpPost]
        public ContentResult Oder(Cart cart, Customer customer)
        {
            try
            {
                if (Session["user"] != null)
                {
                    User user = (User)Session["user"];
                    var unitOfWork = new UnitOfWork(new QLBHDienThoaiEntities());
                    int customerId = 0;
                    if (user.CustormerId == null)
                    {
                        unitOfWork.Customer.Add(customer);
                        unitOfWork.Complete(); // Đã có customer.CustomerID;
                        User account = unitOfWork.Account.GetAccountByUsername(user.Username, user.Password);
                        // customerId = unitOfWork.Customer.GetAll().LastOrDefault().CustomerID;
                        account.CustormerId = customer.CustomerID;
                        account.RoleId = RoleKey.Customer; //Cập nhật role
                        unitOfWork.Complete();
                        Session["user"] = account;
                    }
                    else
                    {
                        //Customer customerUpdate = unitOfWork.Customer.Get(user.CustormerId.GetValueOrDefault());
                        //customerUpdate.Address = customer.Address;
                        //customerUpdate.Birthday = customer.Birthday;
                        //customerUpdate.Fullname = customer.Fullname;
                        //customerUpdate.Gender = customer.Gender;
                        //customerUpdate.Phone = customer.Phone;
                        customer.CustomerID = user.CustormerId.GetValueOrDefault();
                        unitOfWork.Customer.Put(customer); unitOfWork.Complete();
                        customerId = user.CustormerId.GetValueOrDefault();
                    }
                    decimal totalPrice = 0;
                    foreach (var item in cart.CartDetails)
                    {
                        if (item.PromotionID != null)
                        {
                            totalPrice += (item.PriceProduct * Convert.ToDecimal(item.Amount) - (item.PriceProduct * Convert.ToDecimal(item.Amount)) * Convert.ToDecimal(unitOfWork.Promotion.Get(item.PromotionID.GetValueOrDefault()).SaleOff.GetValueOrDefault() / 100)).GetValueOrDefault();
                        }
                        else
                        {
                            totalPrice += Convert.ToDecimal(item.PriceProduct * item.Amount);
                        }
                    }
                    var newCart = new Cart { CustomerID = customerId, Date = DateTime.Now, Status = 0, VAT = 0, TotalPrice = totalPrice, PaymentID = cart.PaymentID };
                    unitOfWork.Cart.Add(newCart); unitOfWork.Complete();
                    int cartId = unitOfWork.Cart.GetAll().LastOrDefault().CartID;
                    foreach (var item in cart.CartDetails)
                    {
                        item.CartID = cartId;
                        unitOfWork.CartDetail.Add(item); unitOfWork.Complete();
                    }
                    Session["Cart"] = null;
                    return Content(ResponseData.ToJson(new ResponseData(null, 0, true, null, "Đặt hàng thành công!")));
                }
                return Content(ResponseData.ToJson(new ResponseData(null, 0, false, "/Account/Login", "Đặt hàng thất bại! Lỗi đăng nhập.")));
            }
            catch (Exception ex) { return Content(ResponseData.ToJson(new ResponseData(null, 0, false, ex.Message, "Đặt hàng thất bại!"))); }
        }
    }
}