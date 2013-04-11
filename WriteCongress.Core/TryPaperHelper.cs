using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryPaper;

namespace WriteCongress.Core
{
    public class TryPaperHelper
    {

        public static void SendOrderToTryPaper(Order order)
        {
            SendOrderToTryPaper(order, "https://www.writecongress.us/print/printview");
        }
        public static void SendOrderToTryPaper(Order order, string printViewEndPoint)
        {

            string key = System.Configuration.ConfigurationManager.AppSettings["TryPaperKey"];
            string printUrl = "";
            string printViewSite = printViewEndPoint;

            //set default
            if (string.IsNullOrEmpty(printViewSite))
            {
                printViewSite = "https://www.writecongress.us/print/printview";
            }

            if (order != null && order.OrderDetails != null)
            {

                try
                {

                    TryPaperClient client = new TryPaperClient(key);

                    var batch = new Batch() { Id = "wc-batch" + Guid.NewGuid().ToString() };
                    var batchResponse = client.AddBatch(batch);

                    if (batchResponse.Success)
                    {
                        string batchAddressId = "wc-order" + order.Id;

                        //create return address
                        Address add = new Address()
                           {
                               Name = order.Name,
                               AddressLineOne = order.AddressLineOne,
                               City = order.City,
                               Province = order.State,
                               PostalCode = order.ZipCode
                           };

                        if (!String.IsNullOrEmpty(order.AddressLineTwo))
                        {
                            add.AddressLineTwo = order.AddressLineTwo;
                        }
                        ReturnAddress retAddress = new ReturnAddress();
                        retAddress.Address = add;
                        retAddress.Id = batchAddressId;
                        var addAddressResponse = client.AddReturnAddress(retAddress);

                        if (addAddressResponse.Success)
                        {
                            foreach (var lineItem in order.OrderDetails)
                            {
                                string personName = "";
                                var person = lineItem.Person;

                                if (person.Title.ToLower().Contains("sen"))
                                {
                                    personName = FormatHelper.FormatSenatorName(person.FirstName ?? "", person.LastName ?? "");
                                }
                                if (person.Title.ToLower().Contains("rep"))
                                {
                                    personName = FormatHelper.FormatRepName(person.FirstName ?? "", person.LastName ?? "");
                                }
                                printUrl = String.Format("{0}/{1}", printViewEndPoint.TrimEnd('/'), lineItem.Guid);

                                lineItem.TryPaperBatch = batchResponse.Result.Id;


                                Mailing m = new Mailing();
                                m.Id = Guid.NewGuid().ToString();

                                //set up the recipient
                                m.Recipient = new Address()
                                {
                                    Name = personName,
                                    AddressLineOne = person.MailingAddressOne,
                                    City = person.MailingCity,
                                    Province = person.MailingState,
                                    PostalCode = person.MailingZip
                                };
                                m.Content = Content.FromUrl(printUrl);
                                m.ReturnAddressId = batchAddressId;
                                m.Batch = batchResponse.Result;

                                var mailingReference = client.AddMailing(m);

                                if (!mailingReference.Success)
                                {
                                    lineItem.Note = "Mailing Failed";
                                }

                            }
                        }
                        else
                        {
                            order.Note = "Unable to create return address";
                        }
                    }
                    else
                    {
                        order.Note = "Error unable to create batch ID";
                    }

                }
                catch (System.Exception ex)
                {
                    order.Note = ex.Message + "::" + ex.Source + "::" + ex.StackTrace.ToString();
                }
            }
        }
    }
}
