using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WriteCongress.Web.Models.SmartyStreets
{
    public partial class Recipient
    {
        public Recipient()
        {
        }

        public System.Guid Id { get; set; }
        public string NameOne { get; set; }
        public string NameTwo { get; set; }
        public string NameThree { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string AddressLineThree { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public System.DateTime CreateDateUtc { get; set; }
        public string DeliveryLineOne { get; set; }
        public string DeliveryLineTwo { get; set; }
        public Nullable<int> DeliverySuggestionsCount { get; set; }
        public Nullable<System.DateTime> DeliverySuggestionDate { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryZipCode { get; set; }
        public string DeliveryZipPlusFour { get; set; }
        public string CongressionalDistrict { get; set; }
    }
}