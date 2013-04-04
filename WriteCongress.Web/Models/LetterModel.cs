using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WriteCongress.Core;

namespace WriteCongress.Web.Models
{
    public class LetterModel
    {

        public Letter Letter { get; set; }
        public User User { get; set; }

        public LetterModel()
        {
            this.Letter = new Letter();
            this.User = new User();
        }

        public LetterModel(Letter letter, User user)
        {
            this.User = user;
            this.Letter = letter;
        }

        public string FormatAddress
        {
            get
            {
                string addr = "";

                if (this.User != null)
                {
                    addr = this.User.AddressOne;
                    if (!String.IsNullOrEmpty(this.User.AddressTwo))
                    {
                        ///TODO: clean up
                        addr += "<br/>" + this.User.AddressTwo;
                    }
                        
                }

                return addr;
            }
        }

        public string FormatLocation
        {
            get
            {
                string loc = "";

                if (this.User != null)
                {
                    loc = String.Format("{0}, {1} {2}", this.User.City ?? "", this.User.State ?? "", this.User.ZipCode);

                }

                return loc;
            }
        }




        public string FullName
        {
            get
            {
                string name = "";
                if (this.User != null)
                {
                    name = ((this.User.FirstName ?? "") + " " + (this.User.LastName ?? "")).Trim();
                }

                return name;
            }
        }
    }
}