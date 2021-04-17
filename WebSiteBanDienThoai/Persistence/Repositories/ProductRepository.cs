using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Core.Entity.Repositories;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }


        public QLBHDienThoaiEntities QLBHDienThoaiEntities
        {
            get { return Context as QLBHDienThoaiEntities; }
        }

        public List<Product> GetDataHome()
        {
            var db = QLBHDienThoaiEntities;
           return db.Products.Where(x=>x.Status??false).Take(18).OrderByDescending(n=>n.PriceProduct).ToList();
        }

        public  Product Detial(int ProductID)
        {
            var db = QLBHDienThoaiEntities;
            return db.Products.SingleOrDefault(n => n.ProductID == ProductID);
        }

        public List<Product> Category(int TypeProductID)
        {
            var db = QLBHDienThoaiEntities;
            return db.Products.Where(n => n.TypeProductID == TypeProductID&&(n.Status??false)).OrderByDescending(n => n.PriceProduct).ToList();
        }
      
    }
}