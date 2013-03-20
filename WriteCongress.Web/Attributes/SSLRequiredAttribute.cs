using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WriteCongress
{
    public class SSLRequiredAttribute:FilterAttribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            //if this is a HTTP GET in production and isn't over SSL
            if (request.HttpMethod.Equals("GET",StringComparison.Ordinal) && !request.IsLocal && !request.IsSecureConnection)
            {
                string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectResult(url);
            }
        }
    }
}