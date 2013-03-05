using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteCongress.OpenCongressImport
{
    public class ImportContext : DbContext
    {
        public ImportContext() : base(@"Data Source=KREBS-PC\SQLEXPRESS;Initial Catalog=writecongress_dev;Integrated Security=True;") { }

        public DbSet<Bill1> Bills { get; set; }

    }
}
