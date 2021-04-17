using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Core.Entity.Repositories
{
    public interface IProductRepository :IRepository<Product>
    {
        List<Product> GetDataHome();
        Product Detial(int ProductID);
        List<Product> Category(int TypeProductID);
    }
}
