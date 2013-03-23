using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WriteCongress.Core;

namespace WriteCongress.Web.Controllers
{
    public class LetterController : BaseController
    {
        //
        // GET: /Letter/
        private WriteCongressConnection db = new WriteCongressConnection();

        public ActionResult Index(int id, bool position)
        {
            return View();
        }

    }
}
