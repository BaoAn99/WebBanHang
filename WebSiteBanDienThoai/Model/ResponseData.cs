using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Model
{
    public class ResponseData
    {
        public object obj { get; set; }
        public bool status { get; set; }
        public string exception { get; set; }
        public string mess { get; set; }
        public string total { get; set; }

        public ResponseData(object _obj,decimal _total, bool _status, string _exception, string _mess)
        {
            this.obj = _obj;
            this.total = _total.ToString("#,##0.")+"đ";
            this.status = _status;
            this.exception = _exception;
            this.mess = _mess;
        }
        public ResponseData() {  }
        public static string ToJson(object data)
        {
            var res = JsonConvert.SerializeObject(data, Formatting.None,
                   new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
            return res;
        }
    }
}