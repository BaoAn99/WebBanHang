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
    
    public partial class BillOfSale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BillOfSale()
        {
            this.BillSaleDetails = new HashSet<BillSaleDetail>();
            this.Receipts = new HashSet<Receipt>();
        }
    
        public int BillID { get; set; }
        public Nullable<int> CartID { get; set; }
        public Nullable<System.DateTime> BuyDate { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<int> EmployeeDeliveryID { get; set; }
    
        public virtual Cart Cart { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillSaleDetail> BillSaleDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}