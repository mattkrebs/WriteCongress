using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WriteCongress.Web.Models
{
    public class Account
    {
        public Account(WriteCongress.Core.User user)
        {
            if (user != null)
            {
                this.FirstName = user.FirstName;
                this.LastName = user.LastName;
                this.AddressOne = user.AddressOne;
                this.AddressTwo = user.AddressTwo;
                this.City = user.City;
                this.State = user.State;
                this.ZipCode = user.ZipCode;
                this.Email = user.Email;
                this.Identity = user.Identity;



            }

        }


        public int? Id { get; set; }
        public string Identity { get; set; }

        [MaxLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [MaxLength(50, ErrorMessage = "Address line one cannot be longer than 50 characters")]
        public string AddressOne { get; set; }

        [MaxLength(50, ErrorMessage = "Address line two cannot be longer than 50 characters")]
        public string AddressTwo { get; set; }

        [MaxLength(50, ErrorMessage = "City cannot be longer than 50 characters")]
        public string City { get; set; }

        [MaxLength(2, ErrorMessage = "Postal code cannot be longer than 10 characters")]
        public string State { get; set; }

        [MaxLength(10, ErrorMessage = "Postal code cannot be longer than 10 characters")]
        public string ZipCode { get; set; }

        [MaxLength(80, ErrorMessage = "Email code cannot be longer than 10 characters")]
        public string Email { get; set; }
        public string Password { get; set; }
    }


}