using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Core.Entity.Repositories;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Persistence.Repositories
{
    public class CartDetailRepository : Repository<CartDetail>, ICartDetailRepository
    {
        public CartDetailRepository(QLBHDienThoaiEntities context)
            : base(context)
        {
        }
        public List<CartDetail> GetFollowPromotionId(int promotionId)
        {
            return (from carts in QLBHDienThoaiEntities.CartDetails where carts.PromotionID == promotionId select carts).ToList();
        }
        private QLBHDienThoaiEntities QLBHDienThoaiEntities
        {
            get { return Context as QLBHDienThoaiEntities; }
        }
    }
}