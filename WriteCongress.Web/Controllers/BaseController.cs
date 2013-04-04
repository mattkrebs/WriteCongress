﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WriteCongress.Core;

namespace WriteCongress.Web.Controllers
{
    public class BaseController : Controller
    {
        private WriteCongressConnection _db;
        public WriteCongress.Core.WriteCongressConnection Db
        {
            get
            {
                if (_db == null)
                {
                    _db = new WriteCongressConnection();
                }
                return _db;
            }
        }

        public User AuthenticatedUser { get; private set; }

        protected override void OnAuthorization(AuthorizationContext filterContext) {
            var request = filterContext.RequestContext.HttpContext.Request;
            
            //redirect to https://wwww. as needed
            if (!request.IsLocal) {
                bool canonicalizeRedirect = false;
                string host = request.Url.Host;
                if (!host.StartsWith("www.", StringComparison.OrdinalIgnoreCase)) {
                    canonicalizeRedirect = true;
                    host = "www." + host;
                }

                if (!request.IsSecureConnection) {
                    canonicalizeRedirect = true;
                }

                if (canonicalizeRedirect) {
                    string url = String.Format("https://{0}{1}", host, request.RawUrl);
                    filterContext.Result = new RedirectResult(url);
                    return;
                }
            }


            if (User.Identity.IsAuthenticated) {
                var user = Db.Users.FirstOrDefault(u => u.SessionId == User.Identity.Name);
                if (user != null) {
                    ViewBag.AuthenticatedUser = user;
                    AuthenticatedUser = user;
                    return;
                }
                FormsAuthentication.SignOut();//you were marked as authenticated but that session is no longer valid
            }
            ViewBag.AuthenticatedUser = null;
        }
    }
}
