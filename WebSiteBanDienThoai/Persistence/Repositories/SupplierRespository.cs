using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Core.Entity.Repositories;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Persistence.Repositories
{
    public class SupplierRespository:Repository<Supplier>,ISupplierRepository
    {
        public SupplierRespository(DbContext context) : base(context)
        {
        }
    }
}