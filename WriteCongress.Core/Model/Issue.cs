using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WriteCongress.Core.Model
{
    public class Issue
    {
        public int IssueID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public Boolean Active { get; set; }
        
    }
}
