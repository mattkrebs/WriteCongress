using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WriteCongress.Core;
namespace WriteCongress.Web.Controllers
{
    [SSLRequired]
    public class DataController : BaseController
    {
        [HttpPost]
        public JsonResult ZipCodeInfo(string zipcode) {
            var result = Db.ZipCodes.FirstOrDefault(z => z.PostalCode == zipcode);
            if (result == null) {
                return Json(null, JsonRequestBehavior.DenyGet);
            }
            else {
                return Json(new { result.City, result.StateAbbreviation }, JsonRequestBehavior.DenyGet);
            }
        }
    }
}
