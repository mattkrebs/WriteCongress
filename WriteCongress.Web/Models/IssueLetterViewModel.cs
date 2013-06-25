using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WriteCongress.Core;

namespace WriteCongress.Web.Models
{
    public class IssueLetterViewModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CongressionalDistrict { get; set; }

        public Letter Letter { get; set; }
        public Issue Issue { get; set; }
    }
}