using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WriteCongress.Web.Models;

namespace WriteCongress.Web.Controllers
{
    /// <summary>
    /// User specific stuff should probably go here (like a 'My Account' page, Letter Tracking, etc.)
    /// </summary>
    [Authorize,SSLRequired]
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetLetterPrefill()
        {
            return Json(Db.Users.Where(u => u.SessionId == User.Identity.Name).Select(ui => new
            {
                ui.FirstName,
                ui.LastName,
                ui.AddressOne,
                ui.AddressTwo,
                ui.Email,
                ui.PhoneNumber,
                ui.ZipCode,
                ui.City,
                ui.State
            }).FirstOrDefault(), JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult GetPaymentDetails() {
            if (!String.IsNullOrWhiteSpace(AuthenticatedUser.StripeCustomerId)) {
                var service = new Stripe.StripeCustomerService();
                var customer = service.Get(AuthenticatedUser.StripeCustomerId);
                if (customer != null) {
                    return Json(new {customer.StripeCard.Last4, customer.StripeCard.Type,customer.StripeCard.Name});
                }
            }
            return Json(false);

        }

        [HttpPost]
        public JsonResult UpdatePaymentToken(string token) {

            var service = new Stripe.StripeCustomerService(ConfigurationManager.AppSettings["StripeApiKey"]);
            if (!String.IsNullOrWhiteSpace(AuthenticatedUser.StripeCustomerId)) {
                var customerUpdate = new Stripe.StripeCustomerUpdateOptions();
                customerUpdate.Email = AuthenticatedUser.Email;
                customerUpdate.Description = String.Format("{0} {1}", AuthenticatedUser.FirstName, AuthenticatedUser.LastName);
                customerUpdate.TokenId = token;

                try {
                    service.Update(AuthenticatedUser.StripeCustomerId, customerUpdate);
                }
                catch (Stripe.StripeException se) {
                    Logger.ErrorException(String.Format("updating a payment token failed. user:{0}", AuthenticatedUser.Email), se);
                    return Json(new JsonServiceResult<bool>(false, se.Message));
                }
            }
            else {
                var newCustomer = new Stripe.StripeCustomerCreateOptions();
                newCustomer.Email = AuthenticatedUser.Email;
                newCustomer.Description = String.Format("{0} {1}", AuthenticatedUser.FirstName, AuthenticatedUser.LastName);
                newCustomer.TokenId = token;
                try {
                    var stripeCustomer = service.Create(newCustomer);
                    AuthenticatedUser.StripeCustomerId = stripeCustomer.Id;
                    Db.SaveChanges();
                }
                catch (Stripe.StripeException se) {
                    Logger.ErrorException(String.Format("creating a payment token failed. user:{0}", AuthenticatedUser.Email), se);
                    return Json(new JsonServiceResult<bool>(false, se.Message));
                }
            }
            Logger.Trace("updated a payment token. user:{0}", AuthenticatedUser.Email);
            return Json(new JsonServiceResult<bool>(true));
        }
    }
}
