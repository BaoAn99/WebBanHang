using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class ApproveOrderInput
    {
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeDeliveryId { get; set; }
    }
}