using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebSiteBanDienThoai.Areas.Admin.Models;
using WebSiteBanDienThoai.Common.Utils;
using WebSiteBanDienThoai.Core.Entity;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Controllers
{
    public class DatHangController : Controller
    {
        #region orders - lấy danh sách đặt hàng
        
        public ContentResult GetOrders(OrderPaging page)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

            //Tạo 1 Expression mục đích là Include thêm thông tin CartDetails,Customer vào Cart
            Expression<Func<Cart, object>>[] includes = new Expression<Func<Cart, object>>[2];

            includes[0] = x => x.CartDetails;
            includes[1] = x => x.Customer;

            //lấy danh sách cart và sắp xếp giảm dần theo ID
            var dataResult = page.GetCurrentResult<Cart>(unitOfWork.Cart
                .Include(includes)
                .ToList()
                .OrderByDescending(x => x.Status == 0)
                .ThenByDescending(x => x.CartID).Where(x => x.CustomerID == page.customerId).ToList());
            page.result = dataResult;
            return Content(Data.ToJson(new ResponseData(page, true, "", "")));

        }
        public ContentResult GetOrderDetails(OrderPaging page)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

            //Tạo 1 Expression mục đích là Include thêm thông tin CartDetails,Customer vào Cart
            Expression<Func<CartDetail, object>>[] includes = new Expression<Func<CartDetail, object>>[2];

            includes[0] = x => x.Cart;
            includes[1] = x => x.Promotion;

            //lấy danh sách cart và sắp xếp giảm dần theo ID
            var dataResult = page.GetCurrentResult<CartDetail>(unitOfWork.CartDetail
                .Include(includes)
                .ToList()                
                .Where(x => x.CartID == page.customerId).ToList());
            List<CartDetailJker> output = new List<CartDetailJker>();
            foreach(var item in dataResult)
            {
                if (item.PromotionID != null)
                {
                    item.Promotion = unitOfWork.Promotion.Get(item.PromotionID.GetValueOrDefault());
                }
                if (item.CartID != null)
                {
                    item.Cart = unitOfWork.Cart.Get(item.CartID.GetValueOrDefault());
                }
                Product pro = unitOfWork.Product.Get(item.ProductID.GetValueOrDefault());
                output.Add(new CartDetailJker { Amount = item.Amount, Cart = item.Cart, ProductID = item.ProductID, Product = pro, CartDetailID = item.CartDetailID, CartID = item.CartID, PriceProduct = item.PriceProduct, Promotion = item.Promotion, PromotionID = item.PromotionID });
            }
            page.result = output;
            return Content(Data.ToJson(new ResponseData(page, true, "", "")));
        }
        // Lấy chi tiết hóa đơn
        public ContentResult GetOrder(int orderId)
        {
            //Tạo 1 Expression mục đích là Include thêm thông tin
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            Expression<Func<CartDetail, object>>[] includes = new Expression<Func<CartDetail, object>>[1];

            includes[0] = x => x.Promotion;

            //lấy ra danh sach chi tiết đơn hàng theo Id đơn hàng truyền vào
            var dataResults = unitOfWork.CartDetail
                .Include(includes)
                .Where(x => x.CartID == orderId);

            List<OrderDetailView> output = new List<OrderDetailView>();
            foreach (var item in dataResults) // Duyệt từng đơn hàng
            {
                var pro = unitOfWork.Product.Get(item.ProductID ?? 0); // lấy ra sản phẩm theo Id

                if (pro != null)
                {
                    OrderDetailView elm;
                    var promotion = unitOfWork.Promotion.Get(item.PromotionID ?? 0); // Lấy ra mã giảm giá
                    if (promotion != null)
                    {
                        elm = new
                            OrderDetailView(item, pro.Name, promotion.PromotionName, promotion.SaleOff);
                    }
                    else
                    {
                        elm = new
                           OrderDetailView(item, pro.Name, "Không có KM", 0);
                    }

                    output.Add(elm); // thêm vào dữ liệu xuất ra
                }
            }

            return Content(Data.ToJson(new ResponseData(output, true, "", "")));

        }

        public ContentResult ApproveOrder(ApproveOrderInput input)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

                //===========check CartID has exist in  bill Of Sale ======================

                var cartExist = unitOfWork.BillOfSale.Query(x => x.CartID == input.OrderId).Any();
                if (cartExist)
                {
                    return Content(Data.ToJson(new ResponseData("", false, "", "Đã tồn tại")));
                }

                //=========== Begin Process ======================

                //Tạo 1 Expression mục đích là Include thêm thông tin CartDetails,Customer vào Cart
                Expression<Func<Cart, object>>[] includes = new Expression<Func<Cart, object>>[1];

                includes[0] = x => x.Customer;

                //lấy thông tin cart để approve
                var cart = unitOfWork.Cart
                    .Include(includes)
                    .FirstOrDefault(x => x.CartID == input.OrderId);



                if (cart != null)
                {

                    //===========Add billOf Sale======================

                    var billOfSale = new BillOfSale
                    {
                        CartID = cart.CartID,
                        BuyDate = DateTime.Now,
                        EmployeeID = input.EmployeeId,
                        EmployeeDeliveryID = input.EmployeeDeliveryId,
                        Status = StatusBillKey.InProcess,
                        TotalPrice = cart.TotalPrice

                    };
                    unitOfWork.BillOfSale.Add(billOfSale);
                    unitOfWork.Complete();

                    //===========Add detail======================

                    var cartDetails = unitOfWork.CartDetail
                        .Query(x => x.CartID == cart.CartID);

                    List<BillSaleDetail> billSaleDetails = new List<BillSaleDetail>();
                    // Lấy danh sách cart detail qua billDetail 
                    foreach (var item in cartDetails)
                    {
                        BillSaleDetail billSaleDetail = new BillSaleDetail
                        {
                            BillID = billOfSale.BillID,
                            ProductID = item.ProductID,
                            PromotionID = item.PromotionID,
                            Amount = item.Amount,
                            PriceProduct = item.PriceProduct
                        };
                        billSaleDetails.Add(billSaleDetail);
                    }
                    unitOfWork.BillSaleDetail.AddRange(billSaleDetails);
                    unitOfWork.Complete();

                    //=========== Change status Cart ======================

                    var cartData = unitOfWork.Cart.Get(input.OrderId);
                    cartData.Status = StatusCartKey.Success;
                    unitOfWork.Cart.Update(cartData);
                    unitOfWork.Complete();

                }
                return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không Thành công")));

            }
        }

        public ContentResult CancelOrder(int orderId)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

                var dataResult = unitOfWork.Cart.Get(orderId);
                dataResult.Status = StatusCartKey.Cancel;
                unitOfWork.Cart.Update(dataResult);
                unitOfWork.Complete();

                return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không Thành công")));

            }
        }

        public ContentResult CheckApproveOrder(int orderId)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            //check xem đã apporve chưa
            var cartExist = unitOfWork.BillOfSale.Query(x => x.CartID == orderId).Any();
            return Content(Data.ToJson(new ResponseData(cartExist, true, "", "Đã tồn tại")));

        }
        #endregion orders - lấy danh sách đặt hàng
    }
}