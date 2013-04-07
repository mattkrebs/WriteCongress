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
                model.CityStateZip = order.CityStateZip;
                model.PersonName = String.Format("The Honorable {0} {1}", person.FirstName ?? "", person.LastName ?? "");

                if(!String.IsNullOrEmpty(user.PhoneNumber))
                    model.PhoneNumber = Regex.Replace(user.PhoneNumber, @"^\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*(\d)\D*$","($1$2$3) $4$5$6-$7$8$9$10");

                model.Email = user.Email ?? "";

                model.LetterBody = letter.Body;
                model.RE = letter.Description;




                if (person.Title.ToLower().Contains("sen") && !String.IsNullOrEmpty(person.Address))
                {
                    model.Salutation = String.Format("Dear Senator {0} {1}", person.FirstName ?? "", person.LastName ?? "");
                    //(Room #) (Name) Senate Office Building

                    //parse out address

                    string[] parts = person.Address.Split(' ');
                    string number = "";
                    string name = "";
                    string building = "";
                    if (parts.Length >= 2)
                    {
                        number = parts[0];
                        name = char.ToUpper(parts[1][0]) + parts[1].ToLower().Substring(1);
                    }
                    if (person.Address.ToLower().Contains("senate office building"))
                    {
                        building = "Senate Office Building";
                    }
                    else
                    {
                        //parse out
                        //remove DC
                        string c1 = person.Address.ToLower().Replace("washington dc 20510", "");
                        string[] p = c1.Split(' ');
                        if (p.Length >= 3)
                        {
                            building = char.ToUpper(p[2][0]) + p[2].ToLower().Substring(1);
                        }
                    }


                    //check if courtyard senate office Building
                    model.PersonAddressLineOne = string.Format("{0} {1} {2}", number.Trim(), name.Trim(), building.Trim());
                    model.PersonAddressLineTwo = "United States Senate";
                    model.PersonCityStateZip = "Washington DC, 20510";
                }
                else
                {
                    string[] parts = person.Address.Split(' ');
                    string number = "";
                    string name = "";
                    string stateZipCity = "";
                    if (parts.Length >= 2)
                    {
                        number = parts[0];
                        name = char.ToUpper(parts[1][0]) + parts[1].ToLower().Substring(1);
                    }

                    string[] cpart = person.Address.Split(';');

                    if (cpart.Length >= 2)
                    {
                        stateZipCity = cpart[1].Replace("DC ","DC, ");
                    }
                    else
                    {
                        stateZipCity = "Washington DC, 20515";
                    }
                    model.Salutation = String.Format("Dear Representative {0} {1}", person.FirstName ?? "", person.LastName ?? "");
                    model.PersonAddressLineOne = string.Format("{0} {1} House Office Building", number.Trim(), name.Trim());
                    model.PersonAddressLineTwo = "United States House of Representatives";
                    model.PersonCityStateZip = stateZipCity.Trim();
                }

            }



            return model;
        }

    }
}