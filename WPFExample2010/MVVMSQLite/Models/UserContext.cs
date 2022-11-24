using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Runtime;

namespace MVVMSQLServer.Models
{
 public class UserContext : DbContext
    {
        public DbSet<User> Users {get;set; }


        public UserContext()
            : base("ConnectionLocalDb")
        { }

    }
}
