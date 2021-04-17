﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Core.Entity.Repositories
{
    public interface ITypeProductRepository:IRepository<TypeProduct>
    {
       List<TypeProduct> GetCategory();
    }
}