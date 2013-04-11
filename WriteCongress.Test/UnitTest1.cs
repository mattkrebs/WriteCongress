using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TryPaper;
using System.Configuration;
using WriteCongress.Core;



namespace WriteCongress.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void SendJobToTryPaper()
        {
            try
            {
                WriteCongressConnection db = new WriteCongressConnection();
                //create new user
                User userItem = new User();

                userItem.FirstName = "Test";
                userItem.LastName = "User";
                userItem.Email = "test@writecongress.us";
                userItem.Identity = CryptoHelper.GenerateRandomString(64);

                userItem.AddressOne = "6628 Kingsview Lane N";
                userItem.City = "Maple Grove";
                userItem.State = "MN";
                userItem.ZipCode = "55311";

                db.Users.Add(userItem);
                //db.SaveChanges();

                //create new order

                Order order = new Order();

                order.OrderStatusId = 1;
                order.State = userItem.State;
                order.AddressLineOne = userItem.AddressOne;
                order.ZipCode = userItem.ZipCode;
                order.Name = String.Format("{0} {1}", userItem.FirstName, userItem.LastName);
                order.Email = userItem.Email;
                order.Guid = Guid.NewGuid();
                order.User = userItem;
                order.City = userItem.City;
                order.CreateDateUtc = DateTime.UtcNow;
                order.Ip = "127.0.0.1";
                db.Orders.Add(order);


                int[] persons = new int[] { 303, 293, 364 };
                Decimal[] price = new Decimal[] { 1.99m, 1.49m, 1.49m };

                for (int i = 0; i < persons.Length; i++)
                {
                    OrderDetail od = new OrderDetail();
                    int id = persons[i];
                    var peep = db.People.Where(x => x.PersonId == id).FirstOrDefault();

                    od.Person = peep;
                    od.Price = price[i];
                    od.Order = order;
                    od.LetterId = 3;
                    od.Guid = Guid.NewGuid();
                    od.CreateDateUtc = DateTime.UtcNow;
                    db.OrderDetails.Add(od);

                }

                db.SaveChanges();


                //call try paper
                TryPaperHelper.SendOrderToTryPaper(order);




                //cleanup
                //foreach (var od in order.OrderDetails)
                //{
                  //  db.OrderDetails.Remove(od);
               // }
                db.Orders.Remove(order);
                db.Users.Remove(userItem);
                db.SaveChanges();

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }

            

        }

     
    }
}
