using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriteCongress.Web.Models
{
    public static class StateSelectorModel
    {
        public static List<SelectListItem> States {
            get { 
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem(){Text="State",Value=""});
                list.Add(new SelectListItem() { Text = "Alabama", Value = "AL" });
                list.Add(new SelectListItem() { Text = "Alaska", Value = "AK" });
                list.Add(new SelectListItem() { Text = "Arizona", Value = "AZ" });
                list.Add(new SelectListItem() { Text = "Arkansas", Value = "AR" });
                list.Add(new SelectListItem() { Text = "California", Value = "CA" });
                list.Add(new SelectListItem() { Text = "Colorado", Value = "CO" });
                list.Add(new SelectListItem() { Text = "Connecticut", Value = "CT" });
                list.Add(new SelectListItem() { Text = "Delaware", Value = "DE" });
                list.Add(new SelectListItem() { Text = "District of Columbia", Value = "DC" });
                list.Add(new SelectListItem() { Text = "Florida", Value = "FL" });
                list.Add(new SelectListItem() { Text = "Georgia", Value = "GA" });
                list.Add(new SelectListItem() { Text = "Hawaii", Value = "HI" });
                list.Add(new SelectListItem() { Text = "Idaho", Value = "ID" });
                list.Add(new SelectListItem() { Text = "Illinois", Value = "IL" });
                list.Add(new SelectListItem() { Text = "Indiana", Value = "IN" });
                list.Add(new SelectListItem() { Text = "Iowa", Value = "IA" });
                list.Add(new SelectListItem() { Text = "Kansas", Value = "KS" });
                list.Add(new SelectListItem() { Text = "Kentucky", Value = "KY" });
                list.Add(new SelectListItem() { Text = "Louisiana", Value = "LA" });
                list.Add(new SelectListItem() { Text = "Maine", Value = "ME" });
                list.Add(new SelectListItem() { Text = "Maryland", Value = "MD" });
                list.Add(new SelectListItem() { Text = "Massachusetts", Value = "MA" });
                list.Add(new SelectListItem() { Text = "Michigan", Value = "MI" });
                list.Add(new SelectListItem() { Text = "Minnesota", Value = "MN" });
                list.Add(new SelectListItem() { Text = "Mississippi", Value = "MS" });
                list.Add(new SelectListItem() { Text = "Missouri", Value = "MO" });
                list.Add(new SelectListItem() { Text = "Montana", Value = "MT" });
                list.Add(new SelectListItem() { Text = "Nebraska", Value = "NE" });
                list.Add(new SelectListItem() { Text = "Nevada", Value = "NV" });
                list.Add(new SelectListItem() { Text = "New Hampshire", Value = "NH" });
                list.Add(new SelectListItem() { Text = "New Jersey", Value = "NJ" });
                list.Add(new SelectListItem() { Text = "New Mexico", Value = "NM" });
                list.Add(new SelectListItem() { Text = "New York", Value = "NY" });
                list.Add(new SelectListItem() { Text = "North Carolina", Value = "NC" });
                list.Add(new SelectListItem() { Text = "North Dakota", Value = "ND" });
                list.Add(new SelectListItem() { Text = "Ohio", Value = "OH" });
                list.Add(new SelectListItem() { Text = "Oklahoma", Value = "OK" });
                list.Add(new SelectListItem() { Text = "Oregon", Value = "OR" });
                list.Add(new SelectListItem() { Text = "Pennsylvania", Value = "PA" });
                list.Add(new SelectListItem() { Text = "Rhode Island", Value = "RI" });
                list.Add(new SelectListItem() { Text = "South Carolina", Value = "SC" });
                list.Add(new SelectListItem() { Text = "South Dakota", Value = "SD" });
                list.Add(new SelectListItem() { Text = "Tennessee", Value = "TN" });
                list.Add(new SelectListItem() { Text = "Texas", Value = "TX" });
                list.Add(new SelectListItem() { Text = "Utah", Value = "UT" });
                list.Add(new SelectListItem() { Text = "Vermont", Value = "VT" });
                list.Add(new SelectListItem() { Text = "Virginia", Value = "VA" });
                list.Add(new SelectListItem() { Text = "Washington", Value = "WA" });
                list.Add(new SelectListItem() { Text = "West Virginia", Value = "WV" });
                list.Add(new SelectListItem() { Text = "Wisconsin", Value = "WI" });
                list.Add(new SelectListItem() { Text = "Wyoming", Value = "WY" });
                return list;
            }
        }
    }
}