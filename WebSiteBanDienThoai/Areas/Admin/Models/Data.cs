using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class Data
    {
        public static string ToJson(object data)
        {
            var res = JsonConvert.SerializeObject(data, Formatting.None,
                   new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            return res;
        }
    }
    public class CartDetailJker : CartDetail
    {
        public Product Product { get; set; }
    }
}