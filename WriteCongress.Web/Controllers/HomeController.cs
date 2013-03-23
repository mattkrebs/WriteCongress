﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WriteCongress.Core;
using WriteCongress.Web.Models;

namespace WriteCongress.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Content/
        WriteCongressConnection db = new WriteCongressConnection();
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.TopIssues = db.Issues.Take(3).ToList();
            return View(model);
        }

        public ActionResult Signup() {
            return View();
        }


    }
}
