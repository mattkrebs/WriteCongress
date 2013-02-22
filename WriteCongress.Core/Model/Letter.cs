using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WriteCongress.Core.Model
{
    public class Letter
    {
        public int LetterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Body { get; set; }
        public bool Against { get; set; }
        public bool Active { get; set; }
        public virtual Issue Issue { get; set; }

    }
}
