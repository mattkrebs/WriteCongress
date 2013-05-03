using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stripe;
using WriteCongress.Core;
using WriteCongress.Web.Models;

namespace WriteCongress.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        //
        // GET: /Account/

        //public ActionResult Index()
        //{
            //User user = AuthenticatedUser.Orders
          //  return View(AuthenticatedUser);
        //}
        public ActionResult Index(string f)
        {
            //User user = AuthenticatedUser.Orders
            return View(AuthenticatedUser);
        }


        //public ActionResult Index(Guid orderId)
        //{
            //Guid id = Guid.Parse(orderId);
          //  var order = Db.Orders.Where(o => o.Guid == orderId).FirstOrDefault();
            //return View(order);
        //}


        public ActionResult OrderDetail(Guid id)
        {
            //Guid id = Guid.Parse(orderId);
            var order = Db.Orders.Where(o => o.Guid == id).FirstOrDefault();
            return View(order);
        }



        //public ActionResult OrderDetail(string orderId)
        //{
        //    Guid id = Guid.Parse(orderId);
        //    var order = Db.Orders.Where(o => o.Guid == id).FirstOrDefault();
        //    return View(order);
        //}

        //Show Order status and Description
        //Who it was sent to with pictures
        //PDF Link, Tab for each one?
        //

        private List<Person> GetPersons(string personIds) {
            var people = new List<Person>();
            foreach (var personId in personIds.Split(',')) {
                var person = Db.People.SingleOrDefault(p => p.OpenCongressId == personId);
                if(person!=null) {
                    people.Add(person);
                }
            }
            return people;
        }
        private decimal CalculatePrice(int numberOfLetters) {
            decimal price = 0m;
            if (numberOfLetters >= 1) {
                price += 1.99m;
            }
            if (numberOfLetters >= 2) {
                price += (1.49m*(numberOfLetters - 1));
            }
            return price;
        }

        public JsonResult PlaceOrder(string persons,string letterslug) {
            //get some pieces so we can charge
            var people = GetPersons(persons);
            var totalPrice = CalculatePrice(people.Count);
            var letter = Db.Letters.Where(l => l.Slug == letterslug).Single();

            try {
                var user = AuthenticatedUser;

                Order o = new Order();
                o.Guid = Guid.NewGuid();
                o.UserId = user.Id;
                o.CreateDateUtc = DateTime.UtcNow;
                o.StripeChargeId = o.StripeChargeId;
                o.Ip = Request.UserHostAddress;
                o.UserAgent = Request.UserAgent;
                o.OrderTotal = 0; //TODO: calculate this
                o.OrderStatusId = 1;
                o.Name = String.Format("{0} {1}", user.FirstName, user.LastName);
                o.AddressLineOne = String.Format("{0}", user.AddressOne);
                o.AddressLineTwo = String.Format("{0}", user.AddressTwo);
                o.City = user.City;
                o.State = user.State;
                o.ZipCode = user.ZipCode;
                o.PhoneNumber = user.PhoneNumber;
                o.Email = user.Email;
                o.OrderTotal = totalPrice;

                int personsReceivingLetter = 0;
                
                foreach (var person in people) {
                    if (person != null) {
                        personsReceivingLetter++;

                        OrderDetail od = new OrderDetail();
                        od.Guid = Guid.NewGuid();
                        od.PersonId = person.PersonId;
                        od.CreateDateUtc = DateTime.UtcNow;
                        od.LetterId = letter.LetterId;
                        o.OrderDetails.Add(od);
                        if (personsReceivingLetter == 1) {
                            od.Price = 1.99m;
                        }
                        else {
                            od.Price = 1.49m;
                        }
                    }
                }
                o.OrderTotal = totalPrice;
                

                try
                {
                    //try charging their card
                    var charge = new StripeChargeCreateOptions();
                    charge.Description = String.Format("WriteCongress.us: {0}", letter.Name);
                    charge.CustomerId = AuthenticatedUser.StripeCustomerId;
                    charge.AmountInCents = (int)(totalPrice * 100m);
                    charge.Currency = "usd";
                    var chargeService = new StripeChargeService();
                    var chargeResult = chargeService.Create(charge);

                    o.StripeChargeId = chargeResult.Id;
                }
                catch (Stripe.StripeException se) {
                    var jsr = new JsonServiceResult<bool>(false);
                    jsr.Message = se.Message;
                    return Json(jsr);
                }


                //if CC successfull commit order, if TryPaper errors will handle offline
                Db.Orders.Add(o);
                Db.SaveChanges();

                //send order to trypaper if CC successfull
                if (!string.IsNullOrEmpty(o.StripeChargeId))
                {
                    try
                    {
                        TryPaperHelper.SendOrderToTryPaper(o);
                        o.OrderStatusId = 2;
                    }
                    catch (Exception ex) {
                        Logger.FatalException("error while sending to trypaper", ex);
                        //TODO: do something with this?
                        ///Will appear to user as successful order
                        ///Will need to dig into why TryPaper request did not work
                        if (o.Note != null)
                        {
                            o.Note += String.Format("[error while sending to trypaper][{0}", ex.Message);
                        }
                        
                    }
                }

                //save order  after try paper updates

                Db.SaveChanges();
                
                var result = new JsonServiceResult<Guid>(o.Guid, true);
                return Json(result);
            }
            catch (System.Exception ex) {
                Logger.FatalException("error while saving an order", ex);
                var r = new JsonServiceResult<bool>(false);
                r.Message = "An error occured while saving your order. Please contact support.";
                return Json(r);
            }
        }
    }
}
