//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThongDong_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TOUR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOUR()
        {
            this.BOOKING_DETAIL = new HashSet<BOOKING_DETAIL>();
        }
    
        public int TOUR_ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> DESTINATION_ID { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<int> END_DATE { get; set; }
        public Nullable<int> PRICE_CHILD { get; set; }
        public Nullable<int> PRICE_ADULT { get; set; }
        public string DESCRIPTION { get; set; }
        public string HTML { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKING_DETAIL> BOOKING_DETAIL { get; set; }
        public virtual DESTINATION DESTINATION { get; set; }
    }
}