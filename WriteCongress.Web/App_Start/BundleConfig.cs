﻿using System.Web;
using System.Web.Optimization;

namespace WriteCongress.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/css/site").Include("~/content/site.css").Include("~/content/letter.css"));
            bundles.Add(new StyleBundle("~/css/print").Include( new string[] {"~/content/print.css"})); 
            bundles.Add(new ScriptBundle("~/js/site").Include("~/scripts/site.*"));
            bundles.Add(new ScriptBundle("~/js/plugins").Include("~/scripts/plugins/jquery.*"));

        }
    }
}