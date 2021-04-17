using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Areas.Admin.Models
{
    public class BillDetailView : BillSaleDetail
    {
        public string NameProduct { get; set; }
        public string PromotionName { get; set; }
        public double? SaleOff { get; set; }

        public BillDetailView(BillSaleDetail output, string nameProduct, string promotionName, double? saleOff)
        { 
            this.NameProduct = nameProduct;  
            this.Amount = output.Amount;
            this.ProductID = output.ProductID;
            this.PromotionID = output.PromotionID;
            this.PriceProduct = output.PriceProduct; 
            this.PromotionName = promotionName;
            this.SaleOff = saleOff;
        }

    }
}