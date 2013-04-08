using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteCongress.Core
{
    public class FormatHelper
    {

        public static String FormatSenatorName(String firstName, string lastName)
        {
            string name = "";

            if (!String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName))
            {
                name = String.Format("The Honorable {0} {1}", firstName, lastName);
            }
            return name;

        }

        public static String FormatRepName(String firstName, string lastName)
        {
            string name = "";

            if (!String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName))
            {
                name = String.Format("The Honorable {0} {1}", firstName, lastName);
            }
            return name;

        }
        public static String FormatRepSalutation(String firstName, string lastName)
        {
            string name = "";

            if (!String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName))
            {
                name = String.Format("Dear Representative {0} {1}", firstName, lastName);
            }
            return name;
        }

        public static String FormatSenatorSalutation(String firstName, string lastName)
        {
            string name = "";

            if (!String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName))
            {
                name = String.Format("Dear Senator {0} {1}", firstName, lastName);
            }
            return name;
        }




    }
}
