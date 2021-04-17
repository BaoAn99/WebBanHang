using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class GetAnalytic
    {
        public int ProductID { get; set; }
        public string Name { get; set; } 
        public int Sold { get; set; }
        public decimal Revenue { get; set; } 
    }
}