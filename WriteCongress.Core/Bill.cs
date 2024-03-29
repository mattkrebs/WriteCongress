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
    
    public partial class Bill
    {
        public Bill()
        {
            this.Issues = new HashSet<Issue>();
            this.Letters = new HashSet<Letter>();
        }
    
        public int BillId { get; set; }
        public string BillType { get; set; }
        public int Number { get; set; }
        public int SponsorId { get; set; }
        public string PermaLink { get; set; }
        public Nullable<System.DateTime> LastActionDate { get; set; }
        public string Title { get; set; }
        public int Session { get; set; }
        public int PageViewCount { get; set; }
        public string Ident { get; set; }
        public string TypeNumber { get; set; }
        public string TitleCommon { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<Issue> Issues { get; set; }
        public virtual ICollection<Letter> Letters { get; set; }
    }
}
