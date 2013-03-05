using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using WriteCongress.OpenCongressImport.Models;

namespace WriteCongress.OpenCongressImport
{
    class Program
    {

        static void Main(string[] args)
        {
            List<int> ids = new List<int>();
            try
            {
                HttpWebRequest request = WebRequest.Create("http://api.opencongress.org/bills.json?congress=113") as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(RootObject));
                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    RootObject jsonResponse = objResponse as RootObject;

                    using (var db = new OpenCongressEntities())
                    {
                        foreach (var item in jsonResponse.bills)
                        {
                            AddMemberOfCongress(item.bill.sponsor_id);

                            DateTime? lastAction;
                            DateTime? updated;
                            if (String.IsNullOrEmpty(item.bill.last_action_at))
                                lastAction = null;
                            else
                                lastAction = DateTime.Parse(item.bill.last_action_at);


                            if (String.IsNullOrEmpty(item.bill.updated))
                                updated = null;
                            else
                                updated = DateTime.Parse(item.bill.updated);

                            var bill = new Bill()
                            {
                                BillId = item.bill.id,
                                BillType = item.bill.bill_type,
                                Ident = item.bill.ident == null ? null : (string)item.bill.ident,
                                LastActionDate = lastAction,
                                Number = item.bill.number,
                                PageViewCount = item.bill.page_views_count,
                                PermaLink = item.bill.permalink== null ? null : (string)item.bill.permalink,
                                Session = 113,
                                SponsorId = item.bill.sponsor_id,
                                Status = item.bill.status== null ? null : (string)item.bill.status,
                                Title = item.bill.title_full_common== null ? null : (string)item.bill.title_full_common,
                                TitleCommon = item.bill.title_common== null ? null : (string)item.bill.title_common,
                                TypeNumber = item.bill.typenumber== null ? null : (string)item.bill.typenumber,
                                UpdatedDate = updated

                            };
                            Console.WriteLine("Inserting Bill :" + bill.BillId);
                            db.Bills.Add(bill);
                            db.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Bill Error : {0}", ex.Message));
                Console.ReadLine();
            }        



        }


        public static void AddMemberOfCongress(int id)
        {
            try
                {
                    HttpWebRequest personRequest = WebRequest.Create("http://api.opencongress.org/people.json?person_id=" + id) as HttpWebRequest;
                    using (HttpWebResponse response = personRequest.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                            throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(RootPerson));
                        object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                        RootPerson jsonResponse = objResponse as RootPerson;

                        using (var db = new OpenCongressEntities())
                        {
                            foreach (var item in jsonResponse.people)
                            {
                                var people = new Person()
                                {
                                    Active = true,
                                    BirthDate = item.person.birthday == null ? (DateTime?)null : DateTime.Parse(item.person.birthday),
                                    CongressOffice = item.person.congress_office == null ? null : (string)item.person.congress_office,
                                    Email = item.person.email == null ? null : (string)item.person.email,
                                    Fax = item.person.fax == null ? null : (string)item.person.fax,
                                    FirstName = item.person.firstname == null ? null : (string)item.person.firstname,
                                    Gender = item.person.gender == null ? null : (string)item.person.gender,
                                    LastName = item.person.lastname == null ? null : (string)item.person.lastname,
                                    MiddleInital = item.person.middlename == null ? null : (string)item.person.middlename.Substring(0, 1),
                                    PersonId = item.person.person_id,
                                    Phone = item.person.phone == null ? null : (string)item.person.phone,
                                    Session = 113,
                                    TotalVotes = item.person.total_session_votes,
                                    URL = item.person.url == null ? null : (string)item.person.url,
                                    UserApproval = item.person.user_approval,
                                    VotesDemocratic = item.person.votes_democratic_position,
                                    VotesRepublican = item.person.votes_republican_position,
                                    YoutubeId = item.person.youtube_id == null ? null : (string)item.person.url,

                                };


                                Console.WriteLine(String.Format("Inserting Person :{0} {1}", item.person.firstname, item.person.lastname));

                                
                                if (db.People.Find(people.PersonId) == null)
                                {
                                    db.People.Add(people);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine(String.Format("{0} {1} Already Exists", item.person.firstname, item.person.lastname));
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Person Error : {0}", ex.Message));
                    Console.ReadLine();
                    
                }
        }
    }


    
}
