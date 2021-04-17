using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Core.Entity.Repositories;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Persistence.Repositories
{
    public class TypeProductRepository : Repository<TypeProduct>, ITypeProductRepository
    {
        public TypeProductRepository(QLBHDienThoaiEntities context) : base(context)
        {
        }

        public List<TypeProduct> GetCategory()
        {
            var db = QLBHDienThoaiEntities;
            return db.TypeProducts.ToList();
        }

        public QLBHDienThoaiEntities QLBHDienThoaiEntities
        {
            get { return Context as QLBHDienThoaiEntities; }
        }
    }
}