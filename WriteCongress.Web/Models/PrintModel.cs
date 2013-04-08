using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WriteCongress.Core;

namespace WriteCongress.Web.Models
{
    public class PrintModel
    {

        public Order OrderItem { get; set; }
        public String UserName { get; set; }
        public String AddressLineOne { get; set; }
        public String AddressLineTwo { get; set; }
        public String LetterBody { get; set; }
        public String CityStateZip { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String PersonName { get; set; }
        public String Salutation { get; set; }
        public String PersonAddressLineOne { get; set; }
        public String PersonAddressLineTwo { get; set; }
        public String PersonCityStateZip { get; set; }
        
        public String RE { get; set; }



        //public LetterModel Letter { get; set; }
        public PrintModel()
        {
            this.OrderItem = new Order();
        }

        public static PrintModel Populate(Order order, OrderDetail lineItem, Person person, User user, Letter letter)
        {
            PrintModel model = new PrintModel();

            if (order != null && lineItem != null && person != null && user != null && letter != null)
            {
                model.UserName = order.Name ?? "";
                model.AddressLineOne = order.AddressLineOne ?? "";
                model.AddressLineOne = order.AddressLineOne ?? "";
                model.CityStateZip = String.Format("{0}, {1} {2}", order.City, order.State, order.ZipCode.Left(5));
                model.PersonName = String.Format("The Honorable {0} {1}", person.FirstName ?? "", person.LastName ?? "");

                if(!String.IsNullOrEmpty(user.PhoneNumber))
                    model.PhoneNumber = Regex.Replace(user.PhoneNumber, @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$","($1$2$3) $4$5$6-$7$8$9$10");

                model.Email = user.Email ?? "";

                model.LetterBody = letter.Body;
                model.RE = letter.Description;

                if (person.Title.ToLower().Contains("sen") && !String.IsNullOrEmpty(person.Address))
                {
                    model.Salutation = String.Format("Dear Senator {0} {1}", person.FirstName ?? "", person.LastName ?? "");
                }
                else
                {
                    model.Salutation = String.Format("Dear Representative {0} {1}", person.FirstName ?? "", person.LastName ?? "");
                }

                model.PersonAddressLineOne = person.MailingAddressOne ?? "";
                model.PersonCityStateZip = String.Format("{0}, {1} {2}", person.MailingCity ?? "", person.MailingState ?? "", person.MailingZip ?? "");
            }



            return model;
        }

    }
}