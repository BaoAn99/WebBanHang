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
    
    public partial class Receipt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Receipt()
        {
            this.Pays = new HashSet<Pay>();
            this.ReceiptDetails = new HashSet<ReceiptDetail>();
        }
    
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> BillID { get; set; }
        public int ReceiptID { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual BillOfSale BillOfSale { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pay> Pays { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
