using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Core.Entity.Repositories
{
    //  this.Configuration.LazyLoadingEnabled = false;
    public interface ICartDetailRepository : IRepository<CartDetail>
    {
        List<CartDetail> GetFollowPromotionId(int promotionId);
        //object Include(Expression<Func<CartDetail, object>>[] includes);
    }
}