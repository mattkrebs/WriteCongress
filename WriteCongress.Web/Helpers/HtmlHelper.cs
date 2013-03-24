using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString EmailButton(this HtmlHelper h, string text, string link, Color color)
        {
            var href = "<a href=\"{0}\" style=\"color: #fff;\">{1}</a>";
            href = string.Format(href, link, text);

            var sublink = "<span class=\"sublink\">Broken link? {0}</span>";
            sublink = string.Format(sublink, link);


            var table = "<table><tr><td align=\"center\" width=\"300\" bgcolor=\"{0}\" style=\"background: {0}; padding-top: 6px; padding-right: 10px; padding-bottom: 6px; padding-left: 10px; -webkit-border-radius: 4px; -moz-border-radius: 4px; border-radius: 4px; color: #fff; font-size:14px;font-weight: bold; text-decoration: none; font-family: 'Segoe UI', Calibri, Arial, sans-serif; display: block;\">{1}</td></tr></table>{2}";

            return new MvcHtmlString(String.Format(table, ColorTranslator.ToHtml(color), href, sublink));
        }

    }
}