using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WriteCongress.Core;

namespace WriteCongress.Web.Models
{
    public class IssueModel
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Rank { get; set; }
        public bool FeaturedRank { get; set; }
        public int Featured { get; set; }


        public static List<IssueModel> GetHotTopics()
        {
            WriteCongressConnection db = new Core.WriteCongressConnection();

            //var items = db.Issues.Where(x=>x.Featured).ToList();

            return new List<IssueModel>();
           


        }


    }
}