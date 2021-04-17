using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Model
{
    public class ResponseEmail
    { 
        public bool Status { get; set; }
        public string Code { get; set; }

        public ResponseEmail(bool status, string code)
        {
            this.Status = status;
            this.Code = code;
        }

      
    }

    
}