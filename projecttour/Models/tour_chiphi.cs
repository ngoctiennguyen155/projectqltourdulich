//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace projecttour.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tour_chiphi
    {
        public int chiphi_id { get; set; }
        public int doan_id { get; set; }
        public Nullable<decimal> chiphi_total { get; set; }
        public string chiphi_chitiet { get; set; }
    
        public virtual tour_doan tour_doan { get; set; }
    }
}
