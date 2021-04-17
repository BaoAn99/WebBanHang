using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Model
{
    public class ResponseRegister
    {
        public bool  Status { get; set; } 
        public string Message { get; set; }
        public User Acc { get; set; }
    }
}