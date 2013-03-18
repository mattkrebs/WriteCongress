using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}
