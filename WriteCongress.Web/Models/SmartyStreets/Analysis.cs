using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteCongress.Web.Models.SmartyStreeets
{
    //copied from http://stackoverflow.com/questions/9522044/trying-to-consume-smartystreets-json-with-json-net-cannot-deserialize-json-a

    public class Analysis
    {
        public string dpv_match_code { get; set; }
        public string dpv_footnotes { get; set; }
        public string dpv_cmra { get; set; }
        public string dpv_vacant { get; set; }
        public bool ews_match { get; set; }
        public string footnotes { get; set; }
        public string active { get; set; }
    }
}
