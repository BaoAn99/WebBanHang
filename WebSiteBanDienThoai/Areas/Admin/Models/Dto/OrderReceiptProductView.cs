using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Areas.Admin.Models.Dto
{
    public class OrderReceiptProductView
    {
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public string Describe { get; set; }
        public decimal Price { get; set; }
        public int ProductID { get; set; }
        public int PayDetailID { get; set; }
        public int PayID { get; set; }
    }
}