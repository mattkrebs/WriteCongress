﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WriteCongress.Core;
using WriteCongress.Web.Models;

namespace WriteCongress.Web.Controllers
{
    public class PrintController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrintView(Guid orderItemGuid)
        {


            var lineItem = Db.OrderDetails.Where(x => x.Guid == orderItemGuid).FirstOrDefault();
            PrintModel printModel = new PrintModel();
            if (lineItem != null)
            {
               

                //get other objects
                var person = Db.People.FirstOrDefault(x => x.PersonId == lineItem.PersonId);
                var letter = Db.Letters.FirstOrDefault(x => x.LetterId == lineItem.LetterId);
                var order = Db.Orders.FirstOrDefault(x => x.Id == lineItem.OrderId);
                var user = Db.Users.FirstOrDefault(x => x.Id == 10);



                //check all values and only populate if all  objects where found
                if (person != null && letter != null && user != null)
                {
                    printModel = PrintModel.Populate(order, lineItem,person, user, letter);
                }
                else
                {
                    //handle not found
                    return HttpNotFound("cannot find line item order: " + orderItemGuid);
                }
            }
            else
            {
                //handle not found
                return HttpNotFound("cannot find line item order: " + orderItemGuid);
            }

            return View(printModel);
        }


    }
}
