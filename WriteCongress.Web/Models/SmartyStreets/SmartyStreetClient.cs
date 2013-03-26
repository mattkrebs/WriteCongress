using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Newtonsoft.Json;
using RestSharp;

namespace WriteCongress.Web.Models.SmartyStreets
{
    public class SmartyStreetClient
    {
        private static string uriFormat = "https://api.qualifiedaddress.com/street-address?auth-id={0}&auth-token={1}&street={2}&street2={3}&city={4}&state={5}&zipcode={6}&candidates=3";
        

        public CandidateAddress[] GetSuggestions(Recipient recipient) {
            var t = GetSuggestionsAsync(recipient);
            t.Wait();
            return t.Result;
        }

        public async Task<CandidateAddress[]> GetSuggestionsAsync(Recipient recipient)
        {

            RestClient rc = new RestClient("https://api.qualifiedaddress.com");
            var request = new RestRequest("street-address?auth-id={authId}&auth-token={authToken}", Method.POST);

            //TODO: don't use the trypaper API key, but no biggy for the time being
            request.AddHeader("Accept", "application/json");
            request.AddUrlSegment("authId", "0596d732-5eab-4d01-b5c0-01731c797af6");
            request.AddUrlSegment("authToken", "1e8LNbgJiSiO3wphvte1egvHiQo2+kRvCbmzD1LVORujjKSWBy8wRRJ7q7rMuUcsVB8fo68i932qxmqFoOtTxw==");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new[] {
                    new {
                        street = recipient.AddressLineOne,
                        street2 = recipient.AddressLineTwo,
                        city = recipient.City,
                        state = recipient.Province,
                        zipcode = recipient.PostalCode,
                        candidates = 3
                    }
                });
            



            var response = await rc.ExecuteTaskAsync(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                if (response.ErrorException != null)
                {
                    throw response.ErrorException;
                }
                throw new System.Net.WebException(response.ErrorMessage ?? response.StatusDescription);
            }


            return Json.Decode<CandidateAddress[]>(response.Content);
            //return JsonConvert.DeserializeObject<CandidateAddress[]>(response.Content);

        }

        public static Recipient MergeFirstCandidate(Recipient r,CandidateAddress[] suggestions) {
            r.DeliverySuggestionsCount = suggestions.Length;
            r.DeliverySuggestionDate = DateTime.Now;
            //if (suggestions.Length >2) {
            //    return r;
            //}

            var address = suggestions[0];
            r.DeliveryLineOne = address.delivery_line_1;
            r.DeliveryLineTwo = address.delivery_line_2;
            r.DeliveryCity = address.components.city_name;
            r.DeliveryState = address.components.state_abbreviation;
            r.DeliveryZipCode = address.components.zipcode;
            r.DeliveryZipPlusFour = address.components.plus4_code;

            r.DeliveryPointBarcode = address.delivery_point_barcode;
            r.DeliveryPMBDesignator = address.components.pmb_designator;
            r.DeliveryPoint = address.components.delivery_point;
            r.DeliveryPointCheckDigit = address.components.delivery_point_check_digit;
            r.RecordType = address.metadata.record_type;
            r.ResidentialDeliveryIndicator = address.metadata.rdi;
            r.Latitude = (decimal?)address.metadata.latitude;
            r.Longitude = (decimal?)address.metadata.longitude;
            r.DPVMatchCode = address.analysis.dpv_match_code;
            r.DPVFootnotes = address.analysis.dpv_footnotes;
            r.DPVCommercialMailAgency = address.analysis.dpv_cmra == "Y";
            r.DPVVacant = address.analysis.dpv_vacant == "Y";
            r.DPVActive = address.analysis.active == "Y";


            return r;
        }
    }
}