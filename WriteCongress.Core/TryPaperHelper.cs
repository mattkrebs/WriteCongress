using NLog;
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
            //WriteCongressConnection db = new WriteCongressConnection();
            Logger log = LogManager.GetCurrentClassLogger();
                    

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

                    var batch = new Batch() {Id = String.Format("wc-batch{0}", order.Guid)};
                    var batchResponse = client.AddBatch(batch);

                    

                    if (batchResponse.Success)
                    {
                        log.Trace(String.Format("TryPaper Batch Created Successfully:{0} {1}",batchResponse.Result.Id,order.Id));                     
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

                        var addAddressResponse = client.GetReturnAddress(batchAddressId);

                        if (addAddressResponse.Result == null)
                        {
                            ReturnAddress retAddress = new ReturnAddress();
                            retAddress.Address = add;
                            retAddress.Id = batchAddressId;
                            addAddressResponse = client.AddReturnAddress(retAddress);
                        }

                        if (addAddressResponse.Success)
                        {
                            log.Trace(String.Format("TryPaper Return Address Created Successfully:{0} {1}", addAddressResponse.Result.Id, order.Id));                     

                            foreach (var lineItem in order.OrderDetails)
                            {
                                string personName = "";
                                Person person = new Person();
                                person = lineItem.Person;

                                //go get if null
                                if (person == null)
                                {
                                    WriteCongressConnection db = new WriteCongressConnection();
                                    person = db.People.Where(x => x.PersonId == lineItem.PersonId).FirstOrDefault();
                                }

                                ///TODO: handle null person
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
                                m.Id = lineItem.Guid.ToString();

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
                                    lineItem.TryPaperStatusId = 5;
                                    log.Error(String.Format("TryPaper mailing failed {0} {1}", mailingReference != null ? (mailingReference.Message ?? "") :"", order.Id));
                                    lineItem.Note = "Mailing Failed";
                                }
                                else
                                {
                                    lineItem.TryPaperStatusId = 1;
                                    log.Info("Try Paper Lineitem successfull {0} {1}", order.Id, lineItem.Id);
                                }
                            }
                        }
                        else
                        {
                            log.Error(String.Format("TryPaper could not create return address:{0} {1}", addAddressResponse !=null ? (addAddressResponse.Message ?? "") : "", order.Id));
                            order.Note = String.Format("Unable to create return address:{0}", addAddressResponse.Message); ;
                        }
                    }
                    else
                    {
                        log.Error(String.Format("TryPaper could not create batch:{0} {1}", (batchResponse!= null)  ? (batchResponse.Message ?? "") : "", order.Id));
                        order.Note = "Error unable to create batch ID";
                        order.Note = String.Format("Unable to create batch:{0}", batchResponse !=null ? (batchResponse.Message ?? "") : "");
                    }

                }
                catch (System.Exception ex)
                {
                    log.FatalException(String.Format("TryPaper Error{0}",order.Id),ex);
                    order.Note = ex.Message + "::" + ex.Source + "::" + ex.StackTrace.ToString();
                    throw;
                }
            }
        }
    }
}
