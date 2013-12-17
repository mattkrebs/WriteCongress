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
        public int FeaturedRank { get; set; }
        public bool Featured { get; set; }
        public Issue IssueItem { get; set; }

        public IssueModel(Issue issue)
        {
            if (issue != null)
            {
                this.IssueItem = issue;
                this.Url = issue.Slug ?? "";
                this.Name = issue.Name ?? "";
                this.Active = issue.Active;
                this.Rank = issue.Rank.Value;
                this.Featured = issue.Featured;
                this.FeaturedRank = issue.FeaturedRank.Value;
            }

        }

        public static List<IssueModel> GetFeaturedTopics()
        {

            ///TODO: Add Caching
            List<IssueModel> featuredTopics = new List<IssueModel>();
            WriteCongressConnection db = new Core.WriteCongressConnection();

            var items = db.Issues.Where(x=>x.Featured && x.Active).ToList();
            //check to make sure there are letters

            foreach (var item in items)
            {
                featuredTopics.Add(new IssueModel(item));

            }
            return featuredTopics;
        }

      



    }
}