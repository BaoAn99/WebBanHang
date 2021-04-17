using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Model
{
    public class CommentView:Comment
    {
        public string CustomerName { get; set; }
        public static CommentView Convert(Comment cmt,string CustomerName)
        {
            return new CommentView { CommentID = cmt.CommentID, Content = cmt.Content, CustomerID = cmt.CustomerID, CustomerName = CustomerName, Date = cmt.Date, ProductID = cmt.ProductID };
        }
    }
}