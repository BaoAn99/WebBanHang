
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebSiteBanDienThoai.Areas.Admin.Models;
using WebSiteBanDienThoai.Areas.Admin.Models.Dto;
using WebSiteBanDienThoai.Common.Utils;
using WebSiteBanDienThoai.Core.Entity;
using WebSiteBanDienThoai.Core.Utils;
using WebSiteBanDienThoai.Entity;
using WebSiteBanDienThoai.Persistence.Repositories;

namespace WebSiteBanDienThoai.Areas.Admin.Controllers
{

    public class DashboardController : Controller
    {
        public User GetCurrentUser()
        {
            if (Session[SessionKey.Admin] != null)
            {
                return (User)Session[SessionKey.Admin];
            }
            else
            {
                return new User();
            }
        }

        public ActionResult Index()
        {
            if (Session[SessionKey.Admin] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {

                ViewBag.TenHienThi = "Admin";
                ViewBag.avt = "/FileUploads/images/1.png";
                return View();
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }

        #region bill - lấy danh sách đơn hàng
        public ContentResult GetBills()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

            //Tạo 1 Expression mục đích là Include thêm thông tin Employee vào BillOfSale
            Expression<Func<BillOfSale, object>>[] includes = new Expression<Func<BillOfSale, object>>[1];

            includes[0] = x => x.Employee;

            //lấy danh sách BillOfSale và sắp xếp giảm dần theo ID
            var billOfSales = unitOfWork.BillOfSale
                .Include(includes)
                .ToList()
                .OrderByDescending(x => x.Status == 0)
                .ThenByDescending(x => x.BillID);

            List<BillOutPutView> output = new List<BillOutPutView>();
            foreach (var item in billOfSales)
            {
                var deliveryEmployeeName = unitOfWork.Employee.Get(item.EmployeeDeliveryID ?? 0).Name;
                BillOutPutView billOutPutView = new BillOutPutView(item, deliveryEmployeeName, item.Employee.Name);
                output.Add(billOutPutView);
            }

            return Content(Data.ToJson(new ResponseData(output, true, "", "")));

        }
        public ContentResult GetBillForReceipts()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

            //Tạo 1 Expression mục đích là Include thêm thông tin Employee vào BillOfSale
            Expression<Func<BillOfSale, object>>[] includes = new Expression<Func<BillOfSale, object>>[1];

            includes[0] = x => x.Employee;

            //lấy danh sách BillOfSale và sắp xếp giảm dần theo ID
            var billOfSales = unitOfWork.BillOfSale
                .Include(includes)
                .ToList()
                .OrderByDescending(x => x.Status == 0)
                .ThenByDescending(x => x.BillID);

            List<BillOutPutView> output = new List<BillOutPutView>();
            foreach (var item in billOfSales)
            {
                var deliveryEmployeeName = unitOfWork.Employee.Get(item.EmployeeDeliveryID ?? 0).Name;
                var cart = unitOfWork.Cart.Get(item.CartID.GetValueOrDefault());
                var customerName = unitOfWork.Customer.Get(cart.CustomerID.GetValueOrDefault()).Fullname;
                BillOutPutView billOutPutView = new BillOutPutView(item, deliveryEmployeeName, item.Employee.Name, customerName);
                output.Add(billOutPutView);
            }

            return Content(Data.ToJson(new ResponseData(output, true, "", "")));

        }
        // Lấy chi tiết hóa đơn
        public ContentResult GetBill(int billId)
        {
            //Tạo 1 Expression mục đích là Include thêm thông tin
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            Expression<Func<BillSaleDetail, object>>[] includes = new Expression<Func<BillSaleDetail, object>>[1];

            includes[0] = x => x.Promotion;

            //lấy ra danh sach chi tiết đơn hàng theo Id đơn hàng truyền vào
            var dataResults = unitOfWork.BillSaleDetail
                .Include(includes)
                .Where(x => x.BillID == billId);

            List<BillDetailView> output = new List<BillDetailView>();
            foreach (var item in dataResults) // Duyệt từng đơn hàng
            {
                var pro = unitOfWork.Product.Get(item.ProductID ?? 0); // lấy ra sản phẩm theo Id

                if (pro != null)
                {
                    BillDetailView elm;
                    var promotion = unitOfWork.Promotion.Get(item.PromotionID ?? 0); // Lấy ra mã giảm giá
                    if (promotion != null)
                    {
                        elm = new
                            BillDetailView(item, pro.Name, promotion.PromotionName, promotion.SaleOff);
                    }
                    else
                    {
                        elm = new
                            BillDetailView(item, pro.Name, "Không có KM", 0);
                    }

                    output.Add(elm); // thêm vào dữ liệu xuất ra
                }
            }

            return Content(Data.ToJson(new ResponseData(output, true, "", "")));

        }
        public ContentResult GetBillStatus(int billId)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            //check xem đã apporve chưa
            var billExist = unitOfWork.BillOfSale.Query(x => x.BillID == billId).FirstOrDefault();
            bool status = false;
            if (billExist != null)
            {
                status = billExist.Status == StatusBillKey.Processed;
            }
            return Content(Data.ToJson(new ResponseData(status, true, "", "Đã tồn tại")));
        }
        public ContentResult CancelBill(int billId)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

                var dataResult = unitOfWork.BillOfSale.Get(billId);
                dataResult.Status = StatusBillKey.Cancel;
                unitOfWork.BillOfSale.Update(dataResult);
                unitOfWork.Complete();

                return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không Thành công")));

            }
        }
        public ContentResult ProcessedBill(int billId)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

                var dataResult = unitOfWork.BillOfSale.Get(billId);
                dataResult.Status = StatusBillKey.Processed;
                unitOfWork.BillOfSale.Update(dataResult);
                unitOfWork.Complete();

                return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không Thành công")));

            }
        }
        #endregion

        #region orders - lấy danh sách đặt hàng
        public ContentResult GetOrders()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

            //Tạo 1 Expression mục đích là Include thêm thông tin CartDetails,Customer vào Cart
            Expression<Func<Cart, object>>[] includes = new Expression<Func<Cart, object>>[2];

            includes[0] = x => x.CartDetails;
            includes[1] = x => x.Customer;

            //lấy danh sách cart và sắp xếp giảm dần theo ID
            var dataResult = unitOfWork.Cart
                .Include(includes)
                .ToList()
                .OrderByDescending(x => x.Status == 0)
                .ThenByDescending(x => x.CartID);
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));

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


            ////
            OrderDetailObjView detailObjView = new OrderDetailObjView();

            var cart = unitOfWork.Cart.SingleOrDefault(x => x.CartID == orderId);
            cart.Customer = unitOfWork.Customer.SingleOrDefault(x => x.CustomerID == cart.CustomerID);
            detailObjView.Cart = cart;
            detailObjView.OrderDetailView = output;

            return Content(Data.ToJson(new ResponseData(detailObjView, true, "", "")));

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
                        EmployeeID = GetCurrentUser().EmployeeId,
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
        #endregion

        #region Product
        [HttpGet]
        public ContentResult getProducts()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            Expression<Func<Product, object>>[] includes = { x => x.Supplier, x => x.TypeProduct };
            var dataResult = unitOfWork.Product.Include(includes).OrderByDescending(x => x.ProductID).ToList();
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpGet]
        public ContentResult getProduct(int productID)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Product.Get(productID);
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpGet]
        public ContentResult GetProductFollowBills(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var billDetailList = unitOfWork.BillSaleDetail.GetAll().Where(x => x.BillID == id);
            var dataResult = new List<Product>();
            foreach (var item in billDetailList)
            {
                var pro = unitOfWork.Product.Get(item.ProductID.GetValueOrDefault());
                pro.Amount = item.Amount;
                dataResult.Add(pro);
            }
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpPost]
        public ContentResult putProduct(Product updateProduct)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                unitOfWork.Product.Put(updateProduct);
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", updateProduct.Name + " chỉnh sửa thành công!")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, updateProduct.Name + " chỉnh sửa thất bại!"))); }
        }
        [HttpPost]
        public ContentResult postProduct(Product newProduct)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                if (ModelState.IsValid)
                {

                    unitOfWork.Product.Add(newProduct);
                    unitOfWork.Complete();
                    return Content(Data.ToJson(new ResponseData(null, true, "", newProduct.Name + " thêm mới thành công!")));
                }
                else
                {
                    return Content(Data.ToJson(new ResponseData(null, true, "That bai", newProduct.Name + " thêm mới thất bại!")));
                }

            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, newProduct.Name + " thêm mới thất bại!"))); }
        }
        [HttpPost]
        public ContentResult delProduct(int productID)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                var product = unitOfWork.Product.Get(productID);
                unitOfWork.Product.Remove(product);
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", "")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, ""))); }
        }
        #endregion Product

        #region TypeProduct
        [HttpGet]
        public ContentResult getTypeProducts()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.TypeProduct.GetAll();
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpGet]
        public ContentResult getTypeProduct(int typeProductID)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.TypeProduct.Get(typeProductID);
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpPost]
        public ContentResult putTypeProduct(TypeProduct updateTypeProduct)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                unitOfWork.TypeProduct.Put(updateTypeProduct);
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", updateTypeProduct.TypeProductName + " chỉnh sửa thành công!")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, updateTypeProduct.TypeProductName + " chỉnh sửa thất bại!"))); }
        }
        [HttpPost]
        public ContentResult postTypeProduct(TypeProduct newTypeProduct)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                unitOfWork.TypeProduct.Add(newTypeProduct);
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", newTypeProduct.TypeProductName + " thêm mới thành công!")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, newTypeProduct.TypeProductName + " thêm mới thất bại!"))); }
        }
        [HttpPost]
        public ContentResult delTypeProduct(int typeProductID)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                unitOfWork.TypeProduct.Remove(unitOfWork.TypeProduct.Get(typeProductID));
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", "")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, "Xóa thất bại! Loại sản phẩm đang chứa sản phẩm"))); }
        }
        #endregion TypeProduct

        #region Supplier
        [HttpGet]
        public ContentResult GetSuppliers()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Supplier.GetAll();
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }

        [HttpGet]
        public ContentResult GetSupplier(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Supplier.Get(id);
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }

        [HttpPost]
        public ContentResult AddSupplier(Supplier entity)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Supplier.Add(entity);
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData("", true, "", "")));
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Lỗi")));
            }


        }

        [HttpPost]
        public ContentResult EditSupplier(Supplier entity)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Supplier.Put(entity);
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData("", true, "", "")));
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Lỗi")));
            }
        }
        [HttpPost]
        public ContentResult DelSupplier(int entity)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                // Lấy dữ liệu theo ID
                var dataResult = unitOfWork.Supplier.Get(entity);
                if (dataResult != null)
                {
                    //lấy ràng buộc từ bản product
                    var listFK = unitOfWork.Product.Query(x => x.SupplierID == dataResult.SupplierID);

                    foreach (var item in listFK)
                    {
                        var elm = unitOfWork.Product.Get(item.ProductID);
                        elm.SupplierID = null; //Set cho nhà cung cấp đó thành null
                        elm.Supplier = null;
                        unitOfWork.Product.Update(elm);
                    }
                    //Xóa nhà cung cấp
                    unitOfWork.Supplier.Remove(dataResult);
                    unitOfWork.Complete();
                    return Content(Data.ToJson(new ResponseData("", true, "", "")));
                }
                return Content(Data.ToJson(new ResponseData("", false, "", "Khong ton tai")));

            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Lỗi")));
            }
        }
        #endregion Supplier

        #region Account
        [Permission(Role = RoleKey.Admin)]
        [HttpGet]
        public ContentResult getAccounts(bool isLocked = false)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            if (isLocked)
            {
                var dataResult = unitOfWork.Account.Query(x => x.IsLocked).OrderByDescending(x => x.CustormerId);
                return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
            }
            else
            {

                var dataResult = unitOfWork.Account.Query(x => !x.IsLocked).OrderByDescending(x => x.CustormerId);
                return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
            }

        }

        [Permission(Role = RoleKey.Admin)]
        [HttpGet]
        public ContentResult getAccount(string Username)
        {
            try
            {
                AccountRepository acc = new AccountRepository(new Entity.QLBHDienThoaiEntities());
                var dataResult = acc.Get(Username);
                return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, "", ""))); }

        }
        [HttpGet]
        public ContentResult checkMail(string Email)
        {
            try
            {
                AccountRepository acc = new AccountRepository(new Entity.QLBHDienThoaiEntities());
                var dataResult = acc.checkMail(Email);
                return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, "", ""))); }

        }

        [Permission(Role = RoleKey.Admin)]
        [HttpPost]
        public ContentResult putAccount(User updateUser)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                unitOfWork.Account.Put(updateUser);
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", updateUser.Username + " chỉnh sửa thành công!")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, true, ex.Message, updateUser.Username + " chỉnh sửa thất bại!"))); }
        }

        [Permission(Role = RoleKey.Admin)]
        [HttpPost]
        public ContentResult postAccount(User insertUser)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                unitOfWork.Account.Add(insertUser);
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", insertUser.Username + " thêm thành công!")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, insertUser.Username + " thêm thất bại!"))); }
        }

        [Permission(Role = RoleKey.Admin)]
        [HttpPost]
        public ContentResult deleteAccount(string userName)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                var acc = unitOfWork.Account.GetAll().FirstOrDefault(x => x.Username == userName);
                unitOfWork.Account.Remove(acc);
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", "Xoa Thành công")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, "thất bại!"))); }
        }

        [Permission(Role = RoleKey.Admin)]
        [HttpPost]
        public ContentResult updateRole(int roleId, string username, int customerOrEmployeeId)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                unitOfWork.Account.UpdateRole(roleId, username, customerOrEmployeeId);
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", username + " chỉnh sửa thành công!")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, username + " chỉnh sửa thất bại!"))); }
        }
        #endregion Account

        #region employee
        [HttpGet]
        public ContentResult getEmployees(bool isAdd = false)
        {

            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            if (isAdd)
            {
                var employeeHadAccounts = unitOfWork.Account.Query(x => x.EmployeeId != null).Select(x => x.EmployeeId ?? 0).ToList();
                var dataResult = unitOfWork.Employee.GetAll().Where(x => !employeeHadAccounts.Contains(x.EmployeeID) && x.Type != EmployeeTypeKey.Delivery).ToList();
                return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
            }
            else
            {
                var dataResult = unitOfWork.Employee.GetAll();
                return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));

            }


        }

        [HttpGet]
        public ContentResult getEmployee(int Id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Employee.Get(Id);
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }

        [HttpGet]
        public ContentResult GetEmployeeType(int type)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

            var employees = type == EmployeeTypeKey.Sale
                ? unitOfWork.Employee.Query(x => x.Type == EmployeeTypeKey.Sale).ToList()
                : unitOfWork.Employee.Query(x => x.Type == EmployeeTypeKey.Delivery).ToList();

            return Content(Data.ToJson(new ResponseData(employees, true, "", "")));
        }
        [HttpPost]
        public ContentResult AddEmployee(Employee epl)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Employee.Add(epl);
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return Content(Data.ToJson(new ResponseData("", false, ex.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        [HttpPost]
        public ContentResult EditEmployee(Employee epl)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Employee.Update(epl);
                unitOfWork.Complete();
            }
            catch
            {
                return Content(Data.ToJson(new ResponseData("", false, "", "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }


        [HttpPost]
        public ContentResult DelEmployee(int Id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                var emp = unitOfWork.Employee.Get(Id); // Lấy nhân viên theo Id
                if (emp != null)
                {
                    //Tìm User có chứa mã NV
                    var acc = unitOfWork.Account.GetAccountByEmployeeId(emp.EmployeeID);
                    if (acc != null) // nếu tồn tại User có chứa mã NV
                    {
                        //Set cho mã NV ở bảng User thành Null
                        acc.EmployeeId = null;
                        unitOfWork.Account.Update(acc); // Cập nhật bảng User
                    }
                    // bây giờ đã hết ràng buộc. Tiến hành xóa Nhân viên
                    unitOfWork.Employee.Remove(emp);
                    unitOfWork.Complete();
                }
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        #endregion employee 

        #region customer
        [HttpGet]
        public ContentResult getCustomers(bool isAdd = false)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            if (isAdd)
            {
                var customerHadAccounts = unitOfWork.Account.Query(x => x.CustormerId != null).Select(x => x.CustormerId ?? 0).ToList();
                var dataResult = unitOfWork.Customer.GetAll().Where(x => !customerHadAccounts.Contains(x.CustomerID)).ToList();
                return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
            }
            else
            {
                var dataResult = unitOfWork.Customer.GetAll();
                return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
            }

        }

        [HttpGet]
        public ContentResult getCustomer(int Id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Customer.Get(Id);
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpPost]
        public ContentResult AddCustomer(Customer epl)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Customer.Add(epl);
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return Content(Data.ToJson(new ResponseData("", false, ex.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        [HttpPost]
        public ContentResult EditCustomer(Customer epl)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Customer.Update(epl);
                unitOfWork.Complete();
            }
            catch
            {
                return Content(Data.ToJson(new ResponseData("", false, "", "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }


        [HttpPost]
        public ContentResult DelCustomer(int Id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                var emp = unitOfWork.Customer.Get(Id);
                if (emp != null)
                {
                    var acc = unitOfWork.Account.GetAccountByCustomId(emp.CustomerID);
                    if (acc != null)
                    {
                        acc.CustormerId = null;
                        unitOfWork.Account.Update(acc);
                    }
                    unitOfWork.Customer.Remove(emp);
                    unitOfWork.Complete();
                }
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        #endregion customer

        #region Payments
        [HttpGet]
        public ContentResult GetPayments()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

            var dataResult = unitOfWork.Payment.GetAll();
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));


        }

        [HttpGet]
        public ContentResult GetPayment(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Payment.Get(id);
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpPost]
        public ContentResult AddPayment(Payment elm)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Payment.Add(elm);
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return Content(Data.ToJson(new ResponseData("", false, ex.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        [HttpPost]
        public ContentResult EditPayment(Payment elm)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Payment.Update(elm);
                unitOfWork.Complete();
            }
            catch
            {
                return Content(Data.ToJson(new ResponseData("", false, "", "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }


        [HttpPost]
        public ContentResult DelPayment(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                var elm = unitOfWork.Payment.Get(id);
                if (elm != null)
                {
                    var cartHasPayments = unitOfWork.Cart.Query(x => x.PaymentID == id);
                    foreach (var item in cartHasPayments)
                    {
                        item.PaymentID = null;
                        unitOfWork.Cart.Update(item);
                    }
                    unitOfWork.Payment.Remove(elm);
                    unitOfWork.Complete();
                }
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        #endregion customer

        #region Comments
        [HttpGet]
        public ContentResult GetComments()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

            var dataResult = unitOfWork.Comment.GetAll();
            List<CommentView> commentViews = new List<CommentView>();
            foreach (var item in dataResult)
            {

                var nameProduct = unitOfWork.Product.SingleOrDefault(x => x.ProductID == item.ProductID).Name;
                var customer = unitOfWork.Customer.SingleOrDefault(x => x.CustomerID == item.CustomerID);

                CommentView commentView = new CommentView(item, nameProduct, customer.Fullname);
                commentViews.Add(commentView);
            }

            return Content(Data.ToJson(new ResponseData(commentViews, true, "", "")));


        }

        [HttpGet]
        public ContentResult GetComment(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Comment.Get(id);
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpPost]
        public ContentResult AddComment(Comment elm)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Comment.Add(elm);
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return Content(Data.ToJson(new ResponseData("", false, ex.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        [HttpPost]
        public ContentResult EditComment(Comment elm)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Comment.Update(elm);
                unitOfWork.Complete();
            }
            catch
            {
                return Content(Data.ToJson(new ResponseData("", false, "", "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }


        [HttpPost]
        public ContentResult DelComment(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                var elm = unitOfWork.Comment.Get(id);
                if (elm != null)
                {
                    unitOfWork.Comment.Remove(elm);
                    unitOfWork.Complete();
                }
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        #endregion customer

        #region promotion
        [HttpGet]
        public ContentResult GeneratePromotion()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var promotionList = unitOfWork.Promotion.GetAll().ToList();
            string PromotionName = null;
            while (promotionList.Exists(x => x.PromotionName == PromotionName) || PromotionName == null)
            {
                PromotionName = CodeUtils.RandomString();
            }
            return Content(Data.ToJson(new ResponseData(PromotionName, true, "", "")));
        }
        public ContentResult GetPromotions()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Promotion.GetAll();
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }

        [HttpGet]
        public ContentResult GetPromotion(int Id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Promotion.Get(Id);
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpPost]
        public ContentResult AddPromotion(Promotion prom)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Promotion.Add(prom);
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return Content(Data.ToJson(new ResponseData("", false, ex.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        [HttpPost]
        public ContentResult EditPromotion(Promotion prom)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.Promotion.Update(prom);
                unitOfWork.Complete();
            }
            catch
            {
                return Content(Data.ToJson(new ResponseData("", false, "", "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        [HttpPost]
        public ContentResult DelPromotion(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                var prom = unitOfWork.Promotion.Get(id);
                if (prom != null)
                {
                    unitOfWork.Promotion.Remove(prom);
                    unitOfWork.Complete();
                }
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        #endregion promotion

        #region OrderInvoice        
        [HttpGet]
        public ContentResult GetOrdersJker()
        {
            List<OrderReceiptsView> output = new List<OrderReceiptsView>();

            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.OrderInvoice.GetAll().OrderByDescending(x => x.OrderInvoiceID).ToList();


            foreach (var item in dataResult)
            {
                OrderReceiptsView orderReceiptsView = new OrderReceiptsView();

                orderReceiptsView.OrderInvoiceID = item.OrderInvoiceID;
                orderReceiptsView.OrderDate = item.OrderDate;

                var supplierName = unitOfWork.Supplier.Get(item.SupplierID.GetValueOrDefault());
                orderReceiptsView.SupplierName = supplierName.Name;

                var user = unitOfWork.Account.GetAll().Where(x => x.EmployeeId == item.EmployeeID).FirstOrDefault();
                orderReceiptsView.EmployeeName = unitOfWork.Account.GetAccountByUsername(user.Username, user.Password).FullName;

                List<OrderInvoiceDetail> orderInvoiceDetails =
                    unitOfWork.OrderInvoiceDetail.Query(x => x.OrderInvoiceID == item.OrderInvoiceID).ToList();

                List<OrderReceiptProductView> orderReceiptProductViews = new List<OrderReceiptProductView>();

                foreach (var itm in orderInvoiceDetails)
                {

                    OrderReceiptProductView orderReceiptProductView = new OrderReceiptProductView();
                    var product = unitOfWork.Product.Get(itm.ProductID.GetValueOrDefault());
                    orderReceiptProductView.ProductName = product.Name;
                    orderReceiptProductView.Amount = itm.Amount ?? 0;

                    orderReceiptProductViews.Add(orderReceiptProductView);


                }
                orderReceiptsView.Products = orderReceiptProductViews;
                output.Add(orderReceiptsView);
            }
            return Content(Data.ToJson(new ResponseData(output, true, "", "")));
        }
        [HttpGet]
        public ContentResult GetOrderJker(int orderID)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            Expression<Func<OrderInvoice, object>>[] includes = { x => x.Supplier, x => x.Employee };
            var dataResult = unitOfWork.OrderInvoice.Include(includes).Where(x => x.OrderInvoiceID == orderID).FirstOrDefault();
            dataResult.Supplier = unitOfWork.Supplier.Get(dataResult.SupplierID.GetValueOrDefault());
            dataResult.OrderInvoiceDetails = unitOfWork.OrderInvoiceDetail.Query(x => x.OrderInvoiceID == dataResult.OrderInvoiceID).ToList();
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpPost]
        public ContentResult PutOrderJker(OrderInvoice updateOrder)
        {
            try
            {
                UnitOfWork unitOfWorkOne = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                Expression<Func<OrderInvoice, object>>[] includes = { x => x.Supplier, x => x.Employee };
                var dataResult = unitOfWorkOne.OrderInvoice.Include(includes).Where(x => x.OrderInvoiceID == updateOrder.OrderInvoiceID).FirstOrDefault();
                dataResult.Supplier = unitOfWorkOne.Supplier.Get(dataResult.SupplierID.GetValueOrDefault());
                dataResult.OrderInvoiceDetails = unitOfWorkOne.OrderInvoiceDetail.Query(x => x.OrderInvoiceID == dataResult.OrderInvoiceID).ToList();
                List<OrderInvoiceDetail> orderDetailList = updateOrder.OrderInvoiceDetails.ToList();
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                updateOrder.OrderInvoiceDetails.Clear();
                updateOrder.Supplier = null;
                updateOrder.Employee = null;
                unitOfWork.OrderInvoice.Put(updateOrder);
                unitOfWork.Complete();
                //int newOrderId = unitOfWork.OrderInvoice.GetAll().LastOrDefault().OrderInvoiceID;
                foreach (var item in dataResult.OrderInvoiceDetails)
                {
                    if (!orderDetailList.Exists(x => x.InvoiceDetailID == item.InvoiceDetailID))
                    {
                        UnitOfWork unitOfWorkDel = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                        var itemDel = unitOfWorkDel.OrderInvoiceDetail.Get(item.InvoiceDetailID);
                        unitOfWorkDel.OrderInvoiceDetail.Remove(itemDel);
                        unitOfWorkDel.Complete();
                    }
                }
                foreach (var item in orderDetailList)
                {
                    if (item.InvoiceDetailID != 0)
                    {
                        unitOfWork.OrderInvoiceDetail.Put(item); unitOfWork.Complete();
                    }
                    else
                    {
                        item.OrderInvoiceID = updateOrder.OrderInvoiceID;
                        unitOfWork.OrderInvoiceDetail.Add(item); unitOfWork.Complete();
                    }
                };
                return Content(Data.ToJson(new ResponseData(null, true, "", "Hóa đơn nhập mã " + updateOrder.OrderInvoiceID + " chỉnh sửa thành công!")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, updateOrder.OrderInvoiceID + " chỉnh sửa thất bại!"))); }
        }
        [HttpPost]
        public ContentResult PostOrderJker(OrderInvoice newOrder)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                //newOrder.OrderDate = DateTime.Now;
                //if (ModelState.IsValid)
                //{
                List<OrderInvoiceDetail> orderDetailList = newOrder.OrderInvoiceDetails.ToList();
                newOrder.OrderInvoiceDetails.Clear();
                unitOfWork.OrderInvoice.Add(newOrder);
                unitOfWork.Complete();
                int newOrderId = unitOfWork.OrderInvoice.GetAll().LastOrDefault().OrderInvoiceID;
                foreach (var item in orderDetailList) { item.OrderInvoiceID = newOrderId; unitOfWork.OrderInvoiceDetail.Add(item); unitOfWork.Complete(); };
                return Content(Data.ToJson(new ResponseData(null, true, "", newOrder.OrderInvoiceID + " thêm mới thành công!")));
                //}
                //else
                //{
                //    return Content(Data.ToJson(new ResponseData(null, false, "Thất bại", newOrder.OrderInvoiceID + " thêm mới thất bại!")));
                //}

            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, newOrder.OrderInvoiceID + " thêm mới thất bại!"))); }
        }
        [HttpPost]
        public ContentResult DelOrderJker(int orderID)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                unitOfWork.OrderInvoice.Remove(unitOfWork.OrderInvoice.Get(orderID));
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", "")));
            }
            catch (Exception ex) { return Content(Data.ToJson(new ResponseData(null, false, ex.Message, ""))); }
        }
        #endregion OrderInvoice

        #region Receipt
        [HttpGet]
        public ContentResult GetReceipts()
        {
            List<ReceiptView> output = new List<ReceiptView>();
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Receipt.GetAll();
            foreach (var item in dataResult)
            {
                var recycle = new ReceiptView();
                recycle.BillID = item.BillID;
                recycle.CustomerName = unitOfWork.Customer.Get(unitOfWork.Cart.Get(unitOfWork.BillOfSale.Get(item.BillID.GetValueOrDefault()).CartID.GetValueOrDefault()).CustomerID.GetValueOrDefault()).Fullname;
                recycle.Date = item.Date;
                recycle.EmployeeID = item.EmployeeID;
                var user = unitOfWork.Account.GetAll().Where(x => x.EmployeeId == item.EmployeeID).FirstOrDefault();
                recycle.EmployeeName = unitOfWork.Account.GetAccountByUsername(user.Username, user.Password).FullName;
                recycle.ReceiptID = item.ReceiptID;
                recycle.Status = item.Status.GetValueOrDefault();
                recycle.ReceiptDetails = new List<OrderReceiptProductView>();
                var lst = unitOfWork.ReceiptDetail.GetAll().Where(x => x.ReceiptID == item.ReceiptID);
                foreach (var itm in lst)
                {
                    OrderReceiptProductView o = new OrderReceiptProductView();
                    o.Amount = itm.Amount.GetValueOrDefault();
                    o.Describe = itm.Describe;
                    o.ProductName = unitOfWork.Product.Get(itm.ProductID.GetValueOrDefault()).Name;
                    recycle.ReceiptDetails.Add(o);
                }
                output.Add(recycle);
            }
            return Content(Data.ToJson(new ResponseData(output, true, "", "")));
        }

        [HttpGet]
        public ContentResult GetReceipt(int Id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Receipt.Get(Id);
            dataResult.ReceiptDetails = unitOfWork.ReceiptDetail.GetAll().Where(x => x.ReceiptID == dataResult.ReceiptID).ToList();
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpGet]
        public ContentResult GetReceiptForView(int Id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Receipt.Get(Id);
            ReceiptView rv = new ReceiptView();
            rv.BillID = dataResult.BillID;
            rv.CustomerName = dataResult.CustomerName;
            rv.Date = dataResult.Date;
            rv.EmployeeID = dataResult.EmployeeID;
            rv.EmployeeName = unitOfWork.Employee.Get(dataResult.EmployeeID.GetValueOrDefault()).Name;
            rv.Status = dataResult.Status.GetValueOrDefault();
            rv.ReceiptID = dataResult.ReceiptID;
            rv.ReceiptDetails = new List<OrderReceiptProductView>();
            dataResult.ReceiptDetails = new List<ReceiptDetail>();
            dataResult.ReceiptDetails = unitOfWork.ReceiptDetail.GetAll().Where(x => x.ReceiptID == dataResult.ReceiptID).ToList();
            foreach (var item in dataResult.ReceiptDetails)
            {
                OrderReceiptProductView o = new OrderReceiptProductView();
                o.Amount = item.Amount.GetValueOrDefault();
                o.Describe = item.Describe;
                o.Price = 0;
                o.ProductName = unitOfWork.Product.Get(item.ProductID.GetValueOrDefault()).Name;
                o.ProductID = item.ProductID.GetValueOrDefault();
                rv.ReceiptDetails.Add(o);
            }

            return Content(Data.ToJson(new ResponseData(rv, true, "", "")));
        }
        [HttpPost]
        public ContentResult AddReceipt(Receipt receipt)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                List<ReceiptDetail> orderDetailList = receipt.ReceiptDetails.ToList();
                receipt.ReceiptDetails.Clear();
                unitOfWork.Receipt.Add(receipt);
                unitOfWork.Complete();
                int newReceiptId = unitOfWork.Receipt.GetAll().LastOrDefault().ReceiptID;
                foreach (var item in orderDetailList) { item.ReceiptID = newReceiptId; unitOfWork.ReceiptDetail.Add(item); unitOfWork.Complete(); };
                return Content(Data.ToJson(new ResponseData(null, true, "", "Bảo hành mã" + newReceiptId + " thêm mới thành công!")));
            }
            catch (Exception ex)
            {
                return Content(Data.ToJson(new ResponseData("", false, ex.Message, "Không thành công")));
            }
        }
        [HttpPost]
        public ContentResult EditReceipt(Receipt receipt)
        {
            try
            {
                UnitOfWork unitOfWorkOne = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                var dataResult = unitOfWorkOne.Receipt.GetAll().Where(x => x.ReceiptID == receipt.ReceiptID).FirstOrDefault();
                List<ReceiptDetail> receiptDetailList = unitOfWorkOne.ReceiptDetail.GetAll().Where(x => x.ReceiptID == dataResult.ReceiptID).ToList();
                List<ReceiptDetail> receiptDetailList2 = receipt.ReceiptDetails.ToList();
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                receipt.ReceiptDetails.Clear();
                receipt.Employee = null;
                receipt.BillOfSale = null;
                unitOfWork.Receipt.Put(receipt);
                unitOfWork.Complete();
                //int newOrderId = unitOfWork.OrderInvoice.GetAll().LastOrDefault().OrderInvoiceID;
                foreach (var item in receiptDetailList)
                {
                    if (!receiptDetailList2.Exists(x => x.ReceiptDetailID == item.ReceiptDetailID))
                    {
                        UnitOfWork unitOfWorkDel = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                        var itemDel = unitOfWorkDel.ReceiptDetail.Get(item.ReceiptDetailID);
                        unitOfWorkDel.ReceiptDetail.Remove(itemDel);
                        unitOfWorkDel.Complete();
                    }
                }
                foreach (var item in receiptDetailList2)
                {
                    if (item.ReceiptID != 0)
                    {
                        unitOfWork.ReceiptDetail.Put(item); unitOfWork.Complete();
                    }
                    else
                    {
                        item.ReceiptID = receipt.ReceiptID;
                        unitOfWork.ReceiptDetail.Add(item); unitOfWork.Complete();
                    }
                };
            }
            catch
            {
                return Content(Data.ToJson(new ResponseData("", false, "", "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        [HttpPost]
        public ContentResult DelReceipt(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                var prom = unitOfWork.Receipt.Get(id);
                if (prom != null)
                {
                    unitOfWork.Receipt.Remove(prom);
                    unitOfWork.Complete();
                }
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không thể xóa! Phiếu bảo hành")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        #endregion Receipt

        #region ReceiptDetail
        [HttpGet]
        public ContentResult GetReceiptDetails()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.ReceiptDetail.GetAll();
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }

        [HttpGet]
        public ContentResult GetReceiptDetail(int Id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.ReceiptDetail.Get(Id);
            return Content(Data.ToJson(new ResponseData(dataResult, true, "", "")));
        }
        [HttpPost]
        public ContentResult AddReceiptDetail(ReceiptDetail receiptDetail)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.ReceiptDetail.Add(receiptDetail);
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                return Content(Data.ToJson(new ResponseData("", false, ex.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        [HttpPost]
        public ContentResult EditReceiptDetail(ReceiptDetail receiptDetail)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                unitOfWork.ReceiptDetail.Update(receiptDetail);
                unitOfWork.Complete();
            }
            catch
            {
                return Content(Data.ToJson(new ResponseData("", false, "", "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        [HttpPost]
        public ContentResult DelReceiptDetail(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                var prom = unitOfWork.ReceiptDetail.Get(id);
                if (prom != null)
                {
                    unitOfWork.ReceiptDetail.Remove(prom);
                    unitOfWork.Complete();
                }
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        #endregion ReceiptDetail

        #region Pay
        [HttpGet]
        public ContentResult GetPays()
        {
            List<PayView> output = new List<PayView>();
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Pay.GetAll();
            foreach (var item in dataResult)
            {
                var recycle = new PayView();
                recycle.ReceiptID = item.ReceiptID;
                recycle.CustomerName = item.CustomerName;
                recycle.Date = item.Date;
                recycle.EmployeeID = item.EmployeeID;
                var user = unitOfWork.Account.GetAll().Where(x => x.EmployeeId == item.EmployeeID).FirstOrDefault();
                recycle.EmployeeName = unitOfWork.Account.GetAccountByUsername(user.Username, user.Password).FullName;
                recycle.PayID = item.PayID;
                recycle.TotalPrice = item.TotalPrice.GetValueOrDefault();
                recycle.PayDetails = new List<OrderReceiptProductView>();
                var lst = unitOfWork.PayDetail.GetAll().Where(x => x.PayID == item.PayID);
                foreach (var itm in lst)
                {
                    OrderReceiptProductView o = new OrderReceiptProductView();
                    o.Amount = itm.Amount.GetValueOrDefault();
                    o.Describe = itm.Describe;
                    o.Price = itm.Price.GetValueOrDefault();
                    o.ProductName = unitOfWork.Product.Get(itm.ProductID.GetValueOrDefault()).Name;
                    o.ProductID = itm.ProductID.GetValueOrDefault();
                    recycle.PayDetails.Add(o);
                }
                output.Add(recycle);
            }
            return Content(Data.ToJson(new ResponseData(output, true, "", "")));
        }

        [HttpGet]
        public ContentResult GetPay(int Id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            var dataResult = unitOfWork.Pay.Get(Id);
            PayView rv = new PayView();
            rv.ReceiptID = dataResult.ReceiptID;
            rv.CustomerName = dataResult.CustomerName;
            rv.Date = dataResult.Date;
            rv.EmployeeID = dataResult.EmployeeID;
            rv.EmployeeName = unitOfWork.Employee.Get(dataResult.EmployeeID.GetValueOrDefault()).Name;
            rv.PayID = dataResult.PayID;
            rv.PayDetails = new List<OrderReceiptProductView>();
            rv.TotalPrice = dataResult.TotalPrice.GetValueOrDefault();
            dataResult.PayDetails = new List<PayDetail>();
            dataResult.PayDetails = unitOfWork.PayDetail.GetAll().Where(x => x.PayID == dataResult.PayID).ToList();
            foreach (var item in dataResult.PayDetails)
            {
                OrderReceiptProductView o = new OrderReceiptProductView();
                o.Amount = item.Amount.GetValueOrDefault();
                o.Describe = item.Describe;
                o.Price = item.Price.GetValueOrDefault();
                o.ProductName = unitOfWork.Product.Get(item.ProductID.GetValueOrDefault()).Name;
                o.ProductID = item.ProductID.GetValueOrDefault();
                o.PayID = item.PayID;
                o.PayDetailID = item.PayDetailID;
                rv.PayDetails.Add(o);
            }
            return Content(Data.ToJson(new ResponseData(rv, true, "", "")));
        }
        [HttpPost]
        public ContentResult AddPay(Pay Pay)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                List<PayDetail> orderDetailList = Pay.PayDetails.ToList();
                Pay.PayDetails.Clear();
                unitOfWork.Pay.Add(Pay);
                unitOfWork.Complete();
                int newPayId = unitOfWork.Pay.GetAll().LastOrDefault().PayID;
                foreach (var item in orderDetailList) { item.PayID = newPayId; unitOfWork.PayDetail.Add(item); unitOfWork.Complete(); };
                var receipt = unitOfWork.Receipt.Get(Pay.ReceiptID.GetValueOrDefault());
                receipt.Status = true;
                unitOfWork.Complete();
                return Content(Data.ToJson(new ResponseData(null, true, "", "Bảo hành mã" + newPayId + " thêm mới thành công!")));
            }
            catch (Exception ex)
            {
                return Content(Data.ToJson(new ResponseData("", false, ex.Message, "Không thành công")));
            }
        }
        [HttpPost]
        public ContentResult EditPay(Pay Pay)
        {
            try
            {
                UnitOfWork unitOfWorkOne = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                var dataResult = unitOfWorkOne.Pay.GetAll().Where(x => x.PayID == Pay.PayID).FirstOrDefault();
                List<PayDetail> PayDetailList = unitOfWorkOne.PayDetail.GetAll().Where(x => x.PayID == dataResult.PayID).ToList();
                List<PayDetail> PayDetailList2 = Pay.PayDetails.ToList();
                UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                Pay.PayDetails.Clear();
                Pay.Employee = null;
                Pay.Receipt = null;
                unitOfWork.Pay.Put(Pay);
                unitOfWork.Complete();
                foreach (var item in PayDetailList2)
                {
                    unitOfWork.PayDetail.Put(item); unitOfWork.Complete();
                };
            }
            catch
            {
                return Content(Data.ToJson(new ResponseData("", false, "", "Không thành công")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        [HttpPost]
        public ContentResult DelPay(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
            try
            {
                var prom = unitOfWork.Pay.Get(id);

                if (prom != null)
                {
                    prom.PayDetails = new List<PayDetail>();
                    prom.PayDetails = unitOfWork.PayDetail.GetAll().Where(x => x.PayID == prom.PayID).ToList();
                    foreach (var item in prom.PayDetails)
                    {
                        UnitOfWork unitOfWork2 = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                        var o = unitOfWork2.PayDetail.Get(item.PayDetailID);
                        unitOfWork2.PayDetail.Remove(o);
                        unitOfWork2.Complete();
                    }
                    var receipt = unitOfWork.Receipt.Get(prom.ReceiptID.GetValueOrDefault());
                    receipt.Status = false;
                    unitOfWork.Complete();
                    UnitOfWork unitOfWork3 = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
                    prom = unitOfWork3.Pay.Get(id);
                    prom.PayDetails.Clear();
                    prom.Receipt = null;
                    prom.Employee = null;
                    unitOfWork3.Pay.Remove(prom);
                    unitOfWork3.Complete();
                }
            }
            catch (Exception e)
            {
                return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không thể xóa! Phiếu bảo hành")));
            }
            return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        }
        #endregion Pay

        #region Analytic 

        public GetAnalyticSaleByDateObj GetAnalyticProc(DateTime StartTime, DateTime EndTime, bool isList = false)
        {
            var param = new SqlParameter[] {
                new SqlParameter( "@StartTime",StartTime),
               new SqlParameter( "@EndTime",EndTime),
            };
            GetAnalyticSaleByDateObj output = new GetAnalyticSaleByDateObj();

            using (var context = new QLBHDienThoaiEntities())
            {
                var result = context.Database
                    .SqlQuery<GetAnalyticSaleByDate>("sp_GetAnalyticSaleByDate @StartTime,@EndTime", param)
                    .ToList();
                if (isList)
                {
                    output.GetAnalyticSaleByDates = result;
                }

                foreach (var item in result)
                {
                    output.TotalAmount += item.Amount;
                    output.TotalInventory += item.Inventory;
                    output.TotalSold += item.Sold;
                    output.TotalRevenue += item.Revenue;


                }
                output.StartTime = StartTime.ToString("dd/MM/yyyy");
                output.EndTime = EndTime.ToString("dd/MM/yyyy");


            }
            return output;
        }

        [HttpGet]
        public ContentResult GetAnalyticByDate(DateTime? StartTime, DateTime? EndTime)
        {
            DateTime startTime = StartTime ?? DateTime.Now.AddMonths(-1);

            DateTime endTime = EndTime ?? DateTime.Now;

            var output = GetAnalyticProc(startTime, endTime, true);

            return Content(Data.ToJson(new ResponseData(output, true, "", "")));

        }

        [HttpGet]
        public ContentResult GetAnalyticByYear(int? year)
        {
            int _year = year ?? DateTime.Now.Year;
            List<GetAnalyticSaleByDateObj> listData = new List<GetAnalyticSaleByDateObj>();
            for (int i = 1; i <= 12; i++)
            {
                DateTime date = new DateTime(_year, i, 1);
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                listData.Add(GetAnalyticProc(firstDayOfMonth, lastDayOfMonth));

            }
            return Content(Data.ToJson(new ResponseData(listData, true, "", "")));

        }



        private Stream CreateExcelFile(DateTime? StartTime, DateTime? EndTime, bool exist, Stream stream = null)
        {
            DateTime startTime = StartTime ?? DateTime.Now.AddMonths(-1);

            DateTime endTime = EndTime ?? DateTime.Now;

            var output = GetAnalyticProc(startTime, endTime, true);
            var list = output;
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "Hanker";
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "EPP test background";
                // thêm tí comments vào làm màu 
                excelPackage.Workbook.Properties.Comments = "This is my fucking generated Comments";
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("First Sheet");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];
                // Đỗ data vào Excel file
                if (exist)
                {
                    var listData = list.GetAnalyticSaleByDates.Select(x => new GetAnalyticExits
                    {
                        Amount = x.Amount,
                        Inventory = x.Inventory,
                        Name = x.Name,
                        ProductID = x.ProductID,
                        Sold = x.Sold
                    }).ToList(); 
                    workSheet.Cells[8, 1].LoadFromCollection(listData, true, TableStyles.Dark9);

                    BindingFormatForExcelExist(workSheet, list);
                }
                else
                {
                    var listData = list.GetAnalyticSaleByDates.Select(x => new GetAnalytic
                    {
                        Name = x.Name,
                        ProductID = x.ProductID,
                        Sold = x.Sold,
                        Revenue = x.Revenue
                    }).ToList();
                    workSheet.Cells[8, 1].LoadFromCollection(listData, true, TableStyles.Dark9);

                    BindingFormatForExcel(workSheet, list);
                }
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }
        private void BindingFormatForExcelExist(ExcelWorksheet worksheet, GetAnalyticSaleByDateObj input)
        {
            List<GetAnalyticSaleByDate> listItems = input.GetAnalyticSaleByDates;
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header

            int z = 8;

            worksheet.Cells[z, 1].Value = "Mã sản phẩm";
            worksheet.Cells[z, 2].Value = "Tên sản phẩm";
            worksheet.Cells[z, 3].Value = "Số lượng nhập";
            worksheet.Cells[z, 4].Value = "Số lượng bán";
            worksheet.Cells[z, 5].Value = "Tồn kho";

            // Lấy range vào tạo format cho range đó ở đây là từ A7 tới D7
            using (var range = worksheet.Cells["A8:E8"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;

                range.Style.Font.Color.SetColor(Color.DarkViolet);
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 10));
                // Set Border
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }

            // Đỗ dữ liệu từ list vào 
            for (int i = 0; i < listItems.Count; i++)
            {
                var item = listItems[i];
                worksheet.Cells[i + 1 + z, 1].Value = item.ProductID;
                worksheet.Cells[i + 1 + z, 2].Value = item.Name;
                worksheet.Cells[i + 1 + z, 3].Value = item.Amount;
                worksheet.Cells[i + 1 + z, 4].Value = item.Sold;
                worksheet.Cells[i + 1 + z, 5].Value = item.Inventory;
                //Format lại color nế+6u như thỏa điều kiện
                //if (item.Revenue > 10000050)
                //{
                //    Ở đây chúng ta sẽ format lại theo dạng fromRow, fromCol, toRow, toCol
                //    using (var range = worksheet.Cells[i + 2, 1, i + 2, 6])
                //    {
                //        Format text đỏ và đậm
                //        range.Style.Font.Color.SetColor(Color.Red);
                //        range.Style.Font.Bold = true;
                //    }
                //}

            }
            // Format lại định dạng xuất ra ở cột Money 
            // fix lại width của column với minimum width là 15
            worksheet.Cells[1 + z, 1, listItems.Count + 5 + z, 4].AutoFitColumns(15);

            // Thực hiện tính theo formula trong excel
            // Hàm Sum 
            worksheet.Cells[listItems.Count + 3 + z, 3].Value = "Tổng SL nhập :";
            worksheet.Cells[listItems.Count + 3 + z, 4].Formula = "SUM(C"+(z + 1)+":C" + (listItems.Count+ z +1) + ")";
            worksheet.Cells[listItems.Count + 4 + z, 3].Value = "Tổng SL bán:";
            worksheet.Cells[listItems.Count + 4 + z, 4].Formula = "SUM(D" + (z + 1) + ":D" + (listItems.Count + z + 1) + ")";
            worksheet.Cells[listItems.Count + 5 + z, 3].Value = "Tổng tồn kho:";
            worksheet.Cells[listItems.Count + 5 + z, 4].Formula = "SUM(E" + (z + 1) + ":E" + (listItems.Count + z + 1) + ")";

            // Tổng doanh thu
            //worksheet.Cells[listItems.Count + 6 + z, 3].Value = "Tổng doanh thu: ";
            //worksheet.Cells[listItems.Count + 6 + z, 3].Style.Font.Color.SetColor(Color.Red);
            //worksheet.Cells[listItems.Count + 6 + z, 4].Style.Numberformat.Format = "#,##0";
            //worksheet.Cells[listItems.Count + 6 + z, 4].Formula = "SUM(F2:F" + (listItems.Count + 1) + ")";
            //worksheet.Cells[listItems.Count + 6 + z, 4].Style.Font.Color.SetColor(Color.Red);


            // Infor 
            worksheet.Cells[2, 2, 2, 4].Merge = true;
            var cellTitleInf = worksheet.Cells[2, 2, 2, 4];
            cellTitleInf.Value = "Shop Điện thoại Minh Anh";
            cellTitleInf.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cellTitleInf.Style.Font.Color.SetColor(Color.Red);
            cellTitleInf.Style.Border.BorderAround(ExcelBorderStyle.Double);


            //worksheet.Cells[listItems.Count + 6 + 3, 3].Value = "Thống kê từ: ";
            //worksheet.Cells[listItems.Count + 6 + 3, 3].Style.Font.Color.SetColor(Color.Blue);
            //worksheet.Cells[listItems.Count + 6 + 3, 3].AutoFitColumns();
            //worksheet.Cells[listItems.Count + 6 + 3, 4].Value = input.StartTime+ " - "+ input.EndTime;
            //worksheet.Cells[listItems.Count + 6 + 3, 4].AutoFitColumns();
            //worksheet.Cells[listItems.Count + 6 + 3, 4].Style.Font.Color.SetColor(Color.Red);



            worksheet.Cells[3 + 1, 3].Value = "Người xuất: ";
            worksheet.Cells[3 + 1, 3].Style.Font.Color.SetColor(Color.Blue);
            worksheet.Cells[3 + 1, 4].Value = GetCurrentUser().FullName;
            worksheet.Cells[3 + 1, 4].AutoFitColumns();
            worksheet.Cells[3 + 1, 4].Style.Font.Color.SetColor(Color.Red);


            worksheet.Cells[4 + 1, 3].Value = "Ngày xuất: ";
            worksheet.Cells[4 + 1, 3].Style.Font.Color.SetColor(Color.Blue);
            worksheet.Cells[4 + 1, 4].Value = DateTime.Now.ToString("dd/MM/yyy HH:mm:ss");
            worksheet.Cells[4 + 1, 4].AutoFitColumns();
            worksheet.Cells[4 + 1, 4].Style.Font.Color.SetColor(Color.Red);



            worksheet.Cells[5 + 1, 3, 5 + 1, 4].Merge = true;
            var cellTimeInf = worksheet.Cells[5 + 1, 3, 5 + 1, 4];
            cellTimeInf.Value = input.StartTime + " - " + input.EndTime;
            cellTimeInf.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cellTimeInf.Style.Font.Color.SetColor(Color.Red);
            cellTimeInf.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        }


        private void BindingFormatForExcel(ExcelWorksheet worksheet, GetAnalyticSaleByDateObj input)
        {
            List<GetAnalyticSaleByDate> listItems = input.GetAnalyticSaleByDates;
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header

            int z = 8;

            worksheet.Cells[z, 1].Value = "Mã sản phẩm";
            worksheet.Cells[z, 2].Value = "Tên sản phẩm";
            worksheet.Cells[z, 3].Value = "Số lượng bán";
            worksheet.Cells[z, 4].Value = "Thành tiền";

            // Lấy range vào tạo format cho range đó ở đây là từ A7 tới D7
            using (var range = worksheet.Cells["A8:D8"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;

                range.Style.Font.Color.SetColor(Color.DarkViolet);
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 10));
                // Set Border
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }

            // Đỗ dữ liệu từ list vào 
            for (int i = 0; i < listItems.Count; i++)
            {
                var item = listItems[i];
                worksheet.Cells[i + 1 + z, 1].Value = item.ProductID;
                worksheet.Cells[i + 1 + z, 2].Value = item.Name;
                worksheet.Cells[i + 1 + z, 3].Value = item.Sold;
                worksheet.Cells[i + 1 + z, 4].Value = item.Revenue;
                //Format lại color nế+6u như thỏa điều kiện
                //if (item.Revenue > 10000050)
                //{
                //    Ở đây chúng ta sẽ format lại theo dạng fromRow, fromCol, toRow, toCol
                //    using (var range = worksheet.Cells[i + 2, 1, i + 2, 6])
                //    {
                //        Format text đỏ và đậm
                //        range.Style.Font.Color.SetColor(Color.Red);
                //        range.Style.Font.Bold = true;
                //    }
                //}

            }
            // Format lại định dạng xuất ra ở cột Money
            worksheet.Cells[2 + z, 4, listItems.Count + 4 + z, 4].Style.Numberformat.Format = "#,##0";
            // fix lại width của column với minimum width là 15
            worksheet.Cells[1 + z, 1, listItems.Count + 5 + z, 4].AutoFitColumns(15);

            // Thực hiện tính theo formula trong excel
            // Hàm Sum 

            worksheet.Cells[listItems.Count + 4 + z, 3].Value = "Tổng SL bán:";
            worksheet.Cells[listItems.Count + 4 + z, 4].Formula = "SUM(C8:C" + (listItems.Count + 1+ z) + ")";


            // Tổng doanh thu
            worksheet.Cells[listItems.Count + 5 + z, 3].Value = "Tổng doanh thu: ";
            worksheet.Cells[listItems.Count + 5 + z, 3].Style.Font.Color.SetColor(Color.Red);
            worksheet.Cells[listItems.Count + 5 + z, 4].Style.Numberformat.Format = "#,##0";
            worksheet.Cells[listItems.Count + 5 + z, 4].Formula = "SUM(D8:D" + (listItems.Count + 1 + z) + ")";
            worksheet.Cells[listItems.Count + 5 + z, 4].Style.Font.Color.SetColor(Color.Red);


            // Infor 
            worksheet.Cells[2, 1, 2, 4].Merge = true;
            var cellTitleInf = worksheet.Cells[2, 1, 2, 4];
            cellTitleInf.Value = "Shop Điện thoại Minh Anh";
            cellTitleInf.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cellTitleInf.Style.Font.Color.SetColor(Color.Red);
            cellTitleInf.Style.Border.BorderAround(ExcelBorderStyle.Double);


            //worksheet.Cells[listItems.Count + 6 + 3, 3].Value = "Thống kê từ: ";
            //worksheet.Cells[listItems.Count + 6 + 3, 3].Style.Font.Color.SetColor(Color.Blue);
            //worksheet.Cells[listItems.Count + 6 + 3, 3].AutoFitColumns();
            //worksheet.Cells[listItems.Count + 6 + 3, 4].Value = input.StartTime+ " - "+ input.EndTime;
            //worksheet.Cells[listItems.Count + 6 + 3, 4].AutoFitColumns();
            //worksheet.Cells[listItems.Count + 6 + 3, 4].Style.Font.Color.SetColor(Color.Red);



            worksheet.Cells[3 + 1, 3].Value = "Người xuất: ";
            worksheet.Cells[3 + 1, 3].Style.Font.Color.SetColor(Color.Blue);
            worksheet.Cells[3 + 1, 4].Value = GetCurrentUser().FullName;
            worksheet.Cells[3 + 1, 4].AutoFitColumns();
            worksheet.Cells[3 + 1, 4].Style.Font.Color.SetColor(Color.Red);


            worksheet.Cells[4 + 1, 3].Value = "Ngày xuất: ";
            worksheet.Cells[4 + 1, 3].Style.Font.Color.SetColor(Color.Blue);
            worksheet.Cells[4 + 1, 4].Value = DateTime.Now.ToString("dd/MM/yyy HH:mm:ss");
            worksheet.Cells[4 + 1, 4].AutoFitColumns();
            worksheet.Cells[4 + 1, 4].Style.Font.Color.SetColor(Color.Red);



            worksheet.Cells[5 + 1, 3, 5 + 1, 4].Merge = true;
            var cellTimeInf = worksheet.Cells[5 + 1, 3, 5 + 1, 4];
            cellTimeInf.Value = input.StartTime + " - " + input.EndTime;
            cellTimeInf.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cellTimeInf.Style.Font.Color.SetColor(Color.Red);
            cellTimeInf.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        }



        [HttpGet]
        public ActionResult Export(DateTime? StartTime, DateTime? EndTime, bool exits = false)
        {
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile(StartTime, EndTime, exits);
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=ExcelDemo.xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index :D
            return RedirectToAction("Index");
        }

        #endregion
        #region User
        public ContentResult GetCurrentUserJker()
        {
            if (Session[SessionKey.Admin] != null)
            {
                return Content(Data.ToJson(new ResponseData((User)Session[SessionKey.Admin], true, "", "")));
            }
            return Content(Data.ToJson(new ResponseData(null, false, "", "")));
        }
        #endregion User

    }
}