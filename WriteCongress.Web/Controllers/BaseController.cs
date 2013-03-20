using System;
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
        public WriteCongress.Core.WriteCongressConnection Db {
            get {
                if (_db == null) {
                    _db = new WriteCongressConnection();
                }
                return _db;
            }
        }

        protected override void OnAuthorization(AuthorizationContext filterContext) {
            if (User.Identity.IsAuthenticated) {
                var user = Db.Users.FirstOrDefault(u => u.SessionId == User.Identity.Name);
                if (user != null) {
                    ViewBag.AuthenticatedUser = user;
                    return;
                }
                FormsAuthentication.SignOut();//you were marked as authenticated but that session is no longer valid
            }
            ViewBag.AuthenticatedUser = null;
        }
    }
}
