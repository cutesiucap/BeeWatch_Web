//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Watches
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Watches()
        {
            this.WatchesinCarts = new HashSet<WatchesinCarts>();
        }
    
        public int id { get; set; }
        public string Url_Image { get; set; }
        public Nullable<int> id_Commodities { get; set; }
        public Nullable<int> id_Firms { get; set; }
        public string Name { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> id_discounts { get; set; }
        public Nullable<int> id_Seller { get; set; }
        public string Information { get; set; }
        public Nullable<bool> Lock { get; set; }
        public Nullable<double> Rate { get; set; }
    
        public virtual Accounts Accounts { get; set; }
        public virtual Bought Bought { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Commodities Commodities { get; set; }
        public virtual Discounts Discounts { get; set; }
        public virtual Firms Firms { get; set; }
        public virtual Hot_Trend Hot_Trend { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WatchesinCarts> WatchesinCarts { get; set; }
    }
}
