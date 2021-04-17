using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Core.Entity.Repositories
{
    //  this.Configuration.LazyLoadingEnabled = false;
    public interface ICartRepository : IRepository<Cart>
    {
        //object Include(Expression<Func<Cart, object>>[] includes);
    }
}