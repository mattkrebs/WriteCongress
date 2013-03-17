using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WriteCongress.Web.Models
{
    public class Account
    {
        public int? Id { get; set; }
        public string Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}