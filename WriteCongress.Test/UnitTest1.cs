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
            string key = System.Configuration.ConfigurationManager.AppSettings["TryPaperKey"];

            WriteCongressConnection db = new WriteCongressConnection();

            Guid id = new Guid("51F3DCE4-D1A4-4EF8-8D85-7AE19A4BA911");

            var lineItem = db.OrderDetails.Where(x => x.Guid == id).FirstOrDefault();



            //var order = db.OrderDetails.


            //OrderDetail od = 

            

            TryPaperClient client = new TryPaperClient(key);
            var batch = new Batch() { Id = "Test - Batch" + Guid.NewGuid().ToString() };
            var batchResponse = client.AddBatch(batch);

            Mailing m = new Mailing();
            m.Id = Guid.NewGuid().ToString();
            //set up the recipient
            m.Recipient = new Address()
            {
                Name = String.Format("Fake Cutomer Name, Inc."),
                AddressLineOne = "456 Main St",
                City = "Seattle",
                Province = "WA",
                PostalCode = "98101"
            };
            m.Content = Content.FromByteArray(System.IO.File.ReadAllBytes("invoice.pdf"));
            m.ReturnAddressId = "CorporateHQ";
            //m.Batch = "Feb 2013 Invoices";


            Assert.IsTrue(true);

        }

     
    }
}
