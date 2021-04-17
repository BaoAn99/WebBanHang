using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Areas.Admin.Models.Dto
{
    public class PayView
    {
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<int> ReceiptID { get; set; }
        public string CustomerName { get; set; }
        public int PayID { get; set; }   
        public decimal TotalPrice { get; set; }
        public List<OrderReceiptProductView> PayDetails { get; set; }
    }
}