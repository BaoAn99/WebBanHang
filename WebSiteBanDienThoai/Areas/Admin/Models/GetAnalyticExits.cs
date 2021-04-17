using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class GetAnalyticExits
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int Inventory { get; set; }
        public int Sold { get; set; } 
    }
}