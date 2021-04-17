using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Core.Entity.Repositories;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Persistence.Repositories
{
    public class OrderInvoiceDetailRepository : Repository<OrderInvoiceDetail>, IOrderInvoiceDetailRepository
    {
        public OrderInvoiceDetailRepository(QLBHDienThoaiEntities context)
            : base(context)
        {
        }
    }
}