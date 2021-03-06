//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSiteBanDienThoai.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.BillSaleDetails = new HashSet<BillSaleDetail>();
            this.Comments = new HashSet<Comment>();
            this.OrderInvoiceDetails = new HashSet<OrderInvoiceDetail>();
            this.PayDetails = new HashSet<PayDetail>();
        }
    
        public int ProductID { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> PriceProduct { get; set; }
        public string Images { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> TypeProductID { get; set; }
        public Nullable<int> Amount { get; set; }
        public Nullable<bool> Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillSaleDetail> BillSaleDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderInvoiceDetail> OrderInvoiceDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayDetail> PayDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual TypeProduct TypeProduct { get; set; }
    }
}
