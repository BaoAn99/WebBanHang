using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Core.Entity.Repositories;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Persistence.Repositories
{
    public class CategoryRepository: Repository<TypeProduct>, ICategoryRepository
    {
        public CategoryRepository(QLBHDienThoaiEntities context)
            : base(context)
        {
        }
        public QLBHDienThoaiEntities QLBHDienThoaiEntities
        {
            get { return Context as QLBHDienThoaiEntities; }
        }

        public List<TypeProduct> GetData()
        {
            throw new NotImplementedException();
        }
    }
}