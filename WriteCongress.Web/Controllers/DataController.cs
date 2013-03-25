using System;
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
    public class DataController : BaseController
    {
        [HttpPost]
        public JsonResult ZipCodeInfo(string zipcode) {
            var result = Db.ZipCodes.FirstOrDefault(z => z.PostalCode == zipcode);
            if (result == null) {
                return Json(null, JsonRequestBehavior.DenyGet);
            }
            else {
                return Json(new { result.City, result.StateAbbreviation }, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpPost]
        public JsonResult GetCongressionalDistrictByZip(string zip) {
            var districts = Db.ZipCodeDistricts.Where(z => z.PostalCode == zip).ToList();

            var people = new List<Person>();
            foreach (var d in districts) {
                var person = Db.People.FirstOrDefault(p => p.State == d.State && p.District == d.District);
                if (person != null) {
                    people.Add(person);
                }
            }

            return Json(people, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult GetSenatorsByZip(string zip) {
            var states =Db.ZipCodes.Where(z => z.PostalCode == zip).Select(z => z.StateAbbreviation).ToList();
            
            var people = new List<Person>();
            foreach (var s in states) {
                var senators = Db.People.Where(p => p.State == s && p.District == null).ToList();
                people.AddRange(senators);
            }
            return Json(people, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetCorrectedAddress(string address, string city, string state, string postalcode) {
            Models.SmartyStreets.SmartyStreetClient client = new Models.SmartyStreets.SmartyStreetClient();
            var recipient = new Models.SmartyStreets.Recipient() {
                AddressLineOne = address,
                City = city,
                Province = state,
                PostalCode = postalcode
            };
            var suggestions = client.GetSuggestions(recipient);

            var scrubbedAddress = SmartyStreetClient.MergeFirstCandidate(recipient, suggestions);

            return Json(scrubbedAddress, JsonRequestBehavior.DenyGet);
        }

    }
}
