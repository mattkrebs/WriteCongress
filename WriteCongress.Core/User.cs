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
    
    public partial class User
    {
        public User()
        {
            this.PasswordResets = new HashSet<PasswordReset>();
            this.Orders = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public string Identity { get; set; }
        public Nullable<System.DateTime> CreatedDateUtc { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string SessionId { get; set; }
        public string StripeCustomerId { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public Nullable<int> CongressionalDistrict { get; set; }
    
        public virtual ICollection<PasswordReset> PasswordResets { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
