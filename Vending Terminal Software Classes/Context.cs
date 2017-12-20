using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Terminal_Software_Classes
{
    public class Context : DbContext
    {
        public DbSet<Info> Info { get; set; }
        public DbSet<CurrentState> CurrentStates { get; set; }

        public Context()
            : base("VME")
        { }
    }
}
