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
    
    public partial class ReceiptDetail
    {
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> Amount { get; set; }
        public string Describe { get; set; }
        public int ReceiptDetailID { get; set; }
        public Nullable<int> ReceiptID { get; set; }
    
        public virtual Receipt Receipt { get; set; }
    }
}
