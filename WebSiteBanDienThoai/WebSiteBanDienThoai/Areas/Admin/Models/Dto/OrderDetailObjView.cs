using System.Collections.Generic;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class OrderDetailObjView 
    {
        public Cart Cart { get; set; }
        public List<OrderDetailView> OrderDetailView { get; set; }

    }
}