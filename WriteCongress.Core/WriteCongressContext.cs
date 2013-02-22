using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using WriteCongress.Core.Model;

namespace WriteCongress.Core
{
    public class WriteCongressContext : DbContext
    {
        public DbSet<Letter> Letters { get; set; }
        public DbSet<Issue> Issues { get; set; }


    }
}
