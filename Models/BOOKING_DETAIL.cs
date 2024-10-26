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
    
    public partial class BOOKING_DETAIL
    {
        public int BOOKING_DETAIL_ID { get; set; }
        public Nullable<int> BOOKING_ID { get; set; }
        public Nullable<int> TOUR_ID { get; set; }
        public Nullable<int> BOOKING_PEOPLE { get; set; }
        public Nullable<int> ACCOUNT_ID { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<System.DateTime> END_DATE { get; set; }
        public Nullable<int> PRICE_CHILD { get; set; }
        public Nullable<int> PRICE_ADULT { get; set; }
        public Nullable<int> QUANTITY_ADULT { get; set; }
        public Nullable<int> QUANTITY_CHILD { get; set; }
        public string OTHER_REQUIREMENTS { get; set; }
    
        public virtual ACCOUNT ACCOUNT { get; set; }
        public virtual BOOKING BOOKING { get; set; }
        public virtual TOUR TOUR { get; set; }
    }
}
