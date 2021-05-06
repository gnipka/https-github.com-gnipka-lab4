using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace lab4
{
    class AppContext : DbContext
    {
        public DbSet<Train> Trains { get; set; }

        public AppContext() : base("DefaultConnection") { }
    }
}
