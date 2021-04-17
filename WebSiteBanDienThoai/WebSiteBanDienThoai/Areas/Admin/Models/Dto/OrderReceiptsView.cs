using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Areas.Admin.Models.Dto
{
    public class OrderReceiptsView
    {
        public int OrderInvoiceID { get; set; }
        public DateTime? OrderDate { get; set; }
        public string EmployeeName { get; set; }
        public string SupplierName { get; set; }
        public List<OrderReceiptProductView> Products { get; set; }
    }
}