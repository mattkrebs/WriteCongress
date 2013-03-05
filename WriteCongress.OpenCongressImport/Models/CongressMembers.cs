using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteCongress.OpenCongressImport.Models
{

    public class PersonStats
    {
        public object entered_top_viewed { get; set; }
        public double party_votes_percentage { get; set; }
        public double abstains_percentage { get; set; }
        public int votes_most_often_with_id { get; set; }
        public object entered_top_blog { get; set; }
        public int sponsored_bills_passed_rank { get; set; }
        public int abstains_percentage_rank { get; set; }
        public int cosponsored_bills_passed_rank { get; set; }
        public int abstains { get; set; }
        public int cosponsored_bills { get; set; }
        public int party_votes_percentage_rank { get; set; }
        public int sponsored_bills { get; set; }
        public int votes_least_often_with_id { get; set; }
        public int person_id { get; set; }
        public int cosponsored_bills_rank { get; set; }
        public object entered_top_news { get; set; }
        public int same_party_votes_least_often_with_id { get; set; }
        public int sponsored_bills_passed { get; set; }
        public int sponsored_bills_rank { get; set; }
        public int cosponsored_bills_passed { get; set; }
        public int opposing_party_votes_most_often_with_id { get; set; }
    }

    public class RecentNew
    {
        public object average_rating { get; set; }
        public int commentariable_id { get; set; }
        public string created_at { get; set; }
        public string title { get; set; }
        public string contains_term { get; set; }
        public string excerpt { get; set; }
        public string fti_names { get; set; }
        public string source_url { get; set; }
        public string url { get; set; }
        public object weight { get; set; }
        public string date { get; set; }
        public int id { get; set; }
        public string scraped_from { get; set; }
        public bool is_news { get; set; }
        public string source { get; set; }
        public string status { get; set; }
        public string commentariable_type { get; set; }
        public bool is_ok { get; set; }
    }

    public class RecentBlog
    {
        public object average_rating { get; set; }
        public int commentariable_id { get; set; }
        public string created_at { get; set; }
        public string title { get; set; }
        public string contains_term { get; set; }
        public string excerpt { get; set; }
        public string fti_names { get; set; }
        public string source_url { get; set; }
        public string url { get; set; }
        public object weight { get; set; }
        public string date { get; set; }
        public int id { get; set; }
        public string scraped_from { get; set; }
        public bool is_news { get; set; }
        public string source { get; set; }
        public string status { get; set; }
        public string commentariable_type { get; set; }
        public bool is_ok { get; set; }
    }

    public class Person2
    {
        public string name { get; set; }
        public int votes_democratic_position { get; set; }
        public PersonStats person_stats { get; set; }
        public double with_party_percentage { get; set; }
        public List<RecentNew> recent_news { get; set; }
        public int votes_republican_position { get; set; }
        public object youtube_id { get; set; }
        public object district { get; set; }
        public object url { get; set; }
        public List<RecentBlog> recent_blogs { get; set; }
        public string middlename { get; set; }
        public object watchdog_id { get; set; }
        public string lastname { get; set; }
        public int page_views_count { get; set; }
        public double user_approval { get; set; }
        public object metavid_id { get; set; }
        public string congress_office { get; set; }
        public int oc_users_tracking { get; set; }
        public string gender { get; set; }
        public string contact_webform { get; set; }
        public string bioguide_id { get; set; }
        public string firstname { get; set; }
        public int total_session_votes { get; set; }
        public string fax { get; set; }
        public string phone { get; set; }
        public object sunlight_nickname { get; set; }
        public int person_id { get; set; }
        public int oc_user_comments { get; set; }
        public string birthday { get; set; }
        public string website { get; set; }
        public string religion { get; set; }
        public int blog_article_count { get; set; }
        public string unaccented_name { get; set; }
        public object biography { get; set; }
        public object email { get; set; }
    }

    public class Person1
    {
        public Person2 person { get; set; }
    }

    public class RootPerson
    {
        public int total_pages { get; set; }
        public List<Person1> people { get; set; }
    }
}
