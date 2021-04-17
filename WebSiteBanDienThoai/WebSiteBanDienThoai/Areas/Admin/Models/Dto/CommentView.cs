using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class CommentView : Comment
    {
        public string NameProduct { get; set; }
        public string CustomerName { get; set; }

        public CommentView(Comment output, string nameProduct, string customerName)
        { 
            this.NameProduct = nameProduct;   
            this.ProductID = output.ProductID;
            this.NameProduct = nameProduct;
            this.CustomerName = customerName;
            this.Date = output.Date;
            this.CommentID = output.CommentID;
            this.Content = output.Content;

        }

    }
}