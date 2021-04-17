using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Entity;
using WebSiteBanDienThoai.Persistence.Repositories;

namespace WebSiteBanDienThoai.Core.Entity.Repositories
{
    //  this.Configuration.LazyLoadingEnabled = false;
    public class PayRepository : Repository<Pay>, IPayRepository
    {
        public PayRepository(QLBHDienThoaiEntities context)
            : base(context)
        {
        }
        public QLBHDienThoaiEntities QLBHDienThoaiEntities
        {
            get { return Context as QLBHDienThoaiEntities; }
        }
    }
}