﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WriteCongress.Core;
using WriteCongress.Web.Models.SmartyStreets;

namespace WriteCongress.Web.Controllers
{
    [SSLRequired]
    public class DataController : BaseController {
        public const int OneDayCacheDuration = 60*60*24;
        public const int ThirtyMinuteCacheDuration = 60*30;


        [HttpGet]
        [OutputCache(VaryByParam = "state", Duration = OneDayCacheDuration)]
        public JsonResult SenatorsByState(string state) {
            var senators = Db.People.Where(p => p.State == state && p.District == null).ToList();
            return Json(senators, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        [OutputCache(VaryByParam="zip",Duration=ThirtyMinuteCacheDuration)]
        public JsonResult RepresentativeByZip(string zip)
        {
            var districts = Db.ZipCodeDistricts.Where(z => z.PostalCode == zip).ToList();

            var people = new List<Person>();
            foreach (var d in districts)
            {
                var person = Db.People.FirstOrDefault(p => p.State == d.State && p.District == d.District);
                if (person != null)
                {
                    people.Add(person);
                }
            }

            return Json(people, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(VaryByParam="state;district",Duration=ThirtyMinuteCacheDuration)]
        public JsonResult RepresentativeByStateAndDistrict(string state, int district) {
            var people = Db.People.Where(p => p.State == state && p.District == district).ToList();
            return Json(people, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NormalizedAddress(string address1,string address2, string city, string state, string postalcode) {
            var client = new SmartyStreetClient();
            var recipient = new Recipient() {
                AddressLineOne = address1,
                AddressLineTwo = address2,
                City = city,
                Province = state,
                PostalCode = postalcode
            };
            var suggestions = client.GetSuggestions(recipient);

            var scrubbedAddress = SmartyStreetClient.MergeFirstCandidate(recipient, suggestions);

            return Json(new
            {
                Address1 =scrubbedAddress.DeliveryLineOne,
                Address2=scrubbedAddress.DeliveryLineTwo,
                City=scrubbedAddress.DeliveryCity,
                State=scrubbedAddress.DeliveryState,
                Zip=scrubbedAddress.DeliveryZipCode,
                scrubbedAddress.CongressionalDistrict
            }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult ZipCodeInfo(string zipcode)
        {
            var result = Db.ZipCodes.FirstOrDefault(z => z.PostalCode == zipcode);
            if (result == null)
            {
                //TODO: log this
                return Json(null, JsonRequestBehavior.DenyGet);
            }
            else
            {
                var district = Db.ZipCodeDistricts.Where(z => z.PostalCode == zipcode).ToList();
                int districtNumber = -1;
                
                if (district.Count == 1) {
                    districtNumber = district[0].District;
                }
                return Json(new { result.City, result.StateAbbreviation, CongressionalDistrict = districtNumber }, JsonRequestBehavior.DenyGet);
            }
        }
    }
}
