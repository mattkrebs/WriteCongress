//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WriteCongress.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public System.Guid Guid { get; set; }
        public int OrderId { get; set; }
        public int PersonId { get; set; }
        public Nullable<int> LetterId { get; set; }
        public string TryPaperBatch { get; set; }
        public System.DateTime CreateDateUtc { get; set; }
        public Nullable<decimal> Price { get; set; }
        public bool Processed { get; set; }
        public Nullable<System.DateTime> TryPaperRequestDateUtc { get; set; }
        public string Note { get; set; }
        public int TryPaperStatusId { get; set; }
    
        public virtual Letter Letter { get; set; }
        public virtual Person Person { get; set; }
        public virtual Order Order { get; set; }
    }
}
