using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BehrSite17.Models
{
    public class MainContext : DbContext
    {
        public MainContext()
            : base("MainContext")
        { }

        public DbSet<Specials> Specials { get; set; }
        public DbSet<Backgrounds> Backgrounds { get; set; }

        public System.Data.Entity.DbSet<BehrSite17.Models.BlogPicts> BlogPicts { get; set; }

        public System.Data.Entity.DbSet<BehrSite17.Models.BlogPosts> BlogPosts { get; set; }
    }
}