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
    
    public partial class PasswordReset
    {
        public int Id { get; set; }
        public System.Guid Guid { get; set; }
        public int UserId { get; set; }
        public System.DateTime DateRequestedUtc { get; set; }
        public string UserHostAddress { get; set; }
        public Nullable<System.DateTime> DateUsed { get; set; }
    
        public virtual User User { get; set; }
    }
}
