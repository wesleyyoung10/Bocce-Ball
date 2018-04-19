using Bocce_Ball.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bocce_Ball.Context
{
    class BocceBallDataContext :DbContext
    {
        public BocceBallDataContext():base("name=DefaultConnection")
        {
            
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}
