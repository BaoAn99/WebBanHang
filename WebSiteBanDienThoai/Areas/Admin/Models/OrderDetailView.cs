using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class OrderDetailView : CartDetail
    {
        public string NameProduct { get; set; }
        public string PromotionName { get; set; }
        public double? SaleOff { get; set; } 

        public OrderDetailView(CartDetail output, string nameProduct, string promotionName, double? saleOff)
        {
            this.NameProduct = nameProduct;
            this.CartID = output.CartID;
            this.Amount = output.Amount;
            this.ProductID = output.ProductID;
            this.PromotionID = output.PromotionID;
            this.PriceProduct = output.PriceProduct;
            this.CartDetailID = output.CartDetailID;
            this.PromotionName = promotionName;
            this.SaleOff = saleOff; 
        }

    }
}