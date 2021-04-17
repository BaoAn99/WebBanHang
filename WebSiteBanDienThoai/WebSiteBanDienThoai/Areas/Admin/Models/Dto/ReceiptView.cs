using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Areas.Admin.Models.Dto
{
    public class ReceiptView
    {
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<int> BillID { get; set; }
        public string CustomerName { get; set; }
        public int ReceiptID { get; set; }      
        public bool Status { get; set; }
        public List<OrderReceiptProductView> ReceiptDetails { get; set; }
    }
}