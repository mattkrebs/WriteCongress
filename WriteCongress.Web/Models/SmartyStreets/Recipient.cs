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
        public string DeliveryPointBarcode { get; set; }
        public string DeliveryPRUrbanization { get; set; }
        public string DeliveryPMBDesignator { get; set; }
        public string DeliveryPMBNumber { get; set; }
        public string DeliveryPoint { get; set; }
        public string DeliveryPointCheckDigit { get; set; }
        public string RecordType { get; set; }
        public string ResidentialDeliveryIndicator { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string DPVMatchCode { get; set; }
        public string DPVFootnotes { get; set; }
        public Nullable<bool> DPVCommercialMailAgency { get; set; }
        public Nullable<bool> DPVVacant { get; set; }
        public Nullable<bool> DPVActive { get; set; }
        public Nullable<int> DeliverySuggestionsCount { get; set; }
        public Nullable<System.DateTime> DeliverySuggestionDate { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryZipCode { get; set; }
        public string DeliveryZipPlusFour { get; set; }
    }
}