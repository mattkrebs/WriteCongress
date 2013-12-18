using System.Web;
using System.Web.Optimization;

namespace WriteCongress.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

           bundles.Add(new StyleBundle("~/css/site").Include("~/content/site.css").Include("~/content/letter.css"));

           //bundles.Add(new StyleBundle("~/theme").Include("~//Theme/css/bootstrap.css").Include("~/content/letter.css"));

           StyleBundle themeBundle = new StyleBundle("~/css/theme");

           themeBundle.Include("~/Theme/css/bootstrap.css");
           themeBundle.Include("~/Theme/css/animations.css");
           themeBundle.Include("~/Theme/css/superfish.css");
           themeBundle.Include("~/Theme/css/revolution-slider/css/settings.css");
           themeBundle.Include("~/Theme/css/team-member.css");

           themeBundle.Include("~/Theme/css/prettyPhoto.css");
           themeBundle.Include("~/Theme/css/nivo-slider.css");
           themeBundle.Include("~/Theme/css/style.css");
           themeBundle.Include("~/Theme/css/colors/green.css");
           themeBundle.Include("~/Theme/css/theme-responsive.css");
           
           bundles.Add(themeBundle);


            //bundles.Add(new StyleBundle("~/css/site").Include("~/content/css/Site.css").Include("~/content/letter.css"));
            bundles.Add(new StyleBundle("~/css/print").Include( new string[] {"~/content/print.css"})); 
            bundles.Add(new ScriptBundle("~/js/site").Include("~/scripts/site.*"));
            bundles.Add(new ScriptBundle("~/js/plugins").Include("~/scripts/plugins/jquery.*"));

        }
    }
}