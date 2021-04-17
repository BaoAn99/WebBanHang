using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class GetAnalyticSaleByDateObj
    {
        public List<GetAnalyticSaleByDate> GetAnalyticSaleByDates { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalSold { get; set; }
        public int TotalInventory { get; set; }
        public int TotalAmount { get; set; }
    }
}