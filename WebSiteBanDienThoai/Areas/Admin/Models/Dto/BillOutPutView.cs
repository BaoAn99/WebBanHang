using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Areas.Admin.Models.Dto
{
    public class BillOutPutView:BillOfSale
    {

        public string SaleEmployeeName { get; set; }

        public string DeliveryEmployeeName { get; set; }

        public string FullName { get; set; }

        public BillOutPutView(BillOfSale output, string deliveryEmployeeName, string saleEmployeeName)
        {

            this.DeliveryEmployeeName = deliveryEmployeeName;
            this.SaleEmployeeName = saleEmployeeName;

            this.BillID = output.BillID;
            this.CartID = output.CartID;
            this.BuyDate = output.BuyDate;
            this.Status = output.Status;
            this.TotalPrice = output.TotalPrice;

        }
        public BillOutPutView(BillOfSale output, string deliveryEmployeeName, string saleEmployeeName,string customerName)
        {

            this.DeliveryEmployeeName = deliveryEmployeeName;
            this.SaleEmployeeName = saleEmployeeName;

            this.BillID = output.BillID;
            this.CartID = output.CartID;
            this.BuyDate = output.BuyDate;
            this.Status = output.Status;
            this.TotalPrice = output.TotalPrice;
            this.FullName = customerName;
        }
    }
}