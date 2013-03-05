using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteCongress.OpenCongressImport.Models
{

    public class Bill2
    {
        public string permalink { get; set; }
        public string bill_type { get; set; }
        public int number { get; set; }
        public int sponsor_id { get; set; }
        public string last_action_at { get; set; }
        public int topresident_date { get; set; }
        public string title_full_common { get; set; }
        public object last_vote_roll { get; set; }
        public int session { get; set; }
        public string topresident_datetime { get; set; }
        public object last_speech { get; set; }
        public int id { get; set; }
        public int page_views_count { get; set; }
        public string ident { get; set; }
        public string typenumber { get; set; }
        public object caption { get; set; }
        public object last_vote_date { get; set; }
        public object key_vote_category_id { get; set; }
        public int introduced { get; set; }
        public int news_article_count { get; set; }
        public string summary { get; set; }
        public int blog_article_count { get; set; }
        public string title_common { get; set; }
        public List<string> subjects { get; set; }
        public object plain_language_summary { get; set; }
        public string updated { get; set; }
        public object last_vote_where { get; set; }
        public string status { get; set; }
    }

    public class Bill
    {
        public Bill2 bill { get; set; }
    }

    public class RootObject
    {
        public int total_pages { get; set; }
        public List<Bill> bills { get; set; }
    }

}


