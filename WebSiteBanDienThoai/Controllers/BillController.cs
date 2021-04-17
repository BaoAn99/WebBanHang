using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using WebSiteBanDienThoai.Areas.Admin.Models;
using WebSiteBanDienThoai.Areas.Admin.Models.Dto;
using WebSiteBanDienThoai.Core.Entity;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Controllers
{
    public class BillController : Controller
    {
        #region bill - lấy danh sách đơn hàng
        public ContentResult GetBills(BillPaging page)
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
                item.Cart = unitOfWork.Cart.Get(item.CartID.GetValueOrDefault());
                if (item.Cart.CustomerID == page.customerId)
                {
                    var deliveryEmployeeName = unitOfWork.Employee.Get(item.EmployeeDeliveryID ?? 0).Name;
                    BillOutPutView billOutPutView = new BillOutPutView(item, deliveryEmployeeName, item.Employee.Name);
                    output.Add(billOutPutView);
                }
            }
            page.result = page.GetCurrentResult<BillOutPutView>(output);
            return Content(Data.ToJson(new ResponseData(page, true, "", "")));

        }
        //public FileResult Print(int BillId)
        //{

        //}
        //// Lấy chi tiết hóa đơn
        //public ContentResult GetBill(int billId)
        //{
        //    //Tạo 1 Expression mục đích là Include thêm thông tin
        //    UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
        //    Expression<Func<BillSaleDetail, object>>[] includes = new Expression<Func<BillSaleDetail, object>>[1];

        //    includes[0] = x => x.Promotion;

        //    //lấy ra danh sach chi tiết đơn hàng theo Id đơn hàng truyền vào
        //    var dataResults = unitOfWork.BillSaleDetail
        //        .Include(includes)
        //        .Where(x => x.BillID == billId);

        //    List<BillDetailView> output = new List<BillDetailView>();
        //    foreach (var item in dataResults) // Duyệt từng đơn hàng
        //    {
        //        var pro = unitOfWork.Product.Get(item.ProductID ?? 0); // lấy ra sản phẩm theo Id

        //        if (pro != null)
        //        {
        //            BillDetailView elm;
        //            var promotion = unitOfWork.Promotion.Get(item.PromotionID ?? 0); // Lấy ra mã giảm giá
        //            if (promotion != null)
        //            {
        //                elm = new
        //                    BillDetailView(item, pro.Name, promotion.PromotionName, promotion.SaleOff);
        //            }
        //            else
        //            {
        //                elm = new
        //                    BillDetailView(item, pro.Name, "Không có KM", 0);
        //            }

        //            output.Add(elm); // thêm vào dữ liệu xuất ra
        //        }
        //    }

        //    return Content(Data.ToJson(new ResponseData(output, true, "", "")));

        //}
        //public ContentResult GetBillStatus(int billId)
        //{
        //    UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());
        //    //check xem đã apporve chưa
        //    var billExist = unitOfWork.BillOfSale.Query(x => x.BillID == billId).FirstOrDefault();
        //    bool status = false;
        //    if (billExist != null)
        //    {
        //        status = billExist.Status == StatusBillKey.Processed;
        //    }
        //    return Content(Data.ToJson(new ResponseData(status, true, "", "Đã tồn tại")));
        //}
        //public ContentResult CancelBill(int billId)
        //{
        //    try
        //    {
        //        UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

        //        var dataResult = unitOfWork.BillOfSale.Get(billId);
        //        dataResult.Status = StatusBillKey.Cancel;
        //        unitOfWork.BillOfSale.Update(dataResult);
        //        unitOfWork.Complete();

        //        return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không Thành công")));

        //    }
        //}
        //public ContentResult ProcessedBill(int billId)
        //{
        //    try
        //    {
        //        UnitOfWork unitOfWork = new UnitOfWork(new Entity.QLBHDienThoaiEntities());

        //        var dataResult = unitOfWork.BillOfSale.Get(billId);
        //        dataResult.Status = StatusBillKey.Processed;
        //        unitOfWork.BillOfSale.Update(dataResult);
        //        unitOfWork.Complete();

        //        return Content(Data.ToJson(new ResponseData("", true, "", "Thành công")));
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(Data.ToJson(new ResponseData("", false, e.Message, "Không Thành công")));

        //    }
        //}
        #endregion
    }
}