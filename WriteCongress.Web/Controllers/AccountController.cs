using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WriteCongress.Core;

namespace WriteCongress.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            
            return View();
        }

        public JsonResult PlaceOrder(string[] persons,string letterslug) {
            try {
                var user = AuthenticatedUser;

                Order o = new Order();
                o.Guid = Guid.NewGuid();
                o.UserId = user.Id;
                o.CreateDateUtc = DateTime.UtcNow;
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

                int personsReceivingLetter = 0;
                decimal totalPrice = 0;

                var letterId = Db.Letters.Where(l => l.Slug == letterslug).Select(li => li.LetterId).Single();
                foreach (var personId in persons) {
                    var person = Db.People.FirstOrDefault(p => p.OpenCongressId == personId);
                    
                    if (person != null) {
                        personsReceivingLetter++;

                        OrderDetail od = new OrderDetail();
                        od.Guid = Guid.NewGuid();
                        od.Person = person;
                        od.CreateDateUtc = DateTime.UtcNow;
                        od.LetterId = letterId;
                        o.OrderDetails.Add(od);
                        

                        //TODO: refactor this somewhere, but it's fine for now
                        if (personsReceivingLetter == 1) {
                            od.Price = 1.99m;
                            totalPrice += od.Price.Value;
                        }
                        else {
                            od.Price = 1.49m;
                            totalPrice += od.Price.Value;
                        }

                    }
                }
                o.OrderTotal = totalPrice;
                Db.SaveChanges();
                var result = new JsonServiceResult<Guid>(o.Guid, true);
                return Json(result);
            }
            catch (System.Exception ex) {
                ///TODO: log this
                var result = new JsonServiceResult<Guid>(false, "An error occured");
                return Json(result);
            }
        }
    }
}
