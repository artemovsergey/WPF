using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Configuration;

namespace Commands.Models
{
    class UserContext : DbContext
    {
        
        public virtual DbSet<User> Users { get; set; }


        //public UserContext(): base("Server=localhost,63027;Database=UserDatabase;Trusted_Connection=True;")

        public UserContext(): base("Server=(localdb)\\mssqllocaldb;Database=UserDatabase;Trusted_Connection=True;")
        {
            //Database.Create();
        }


    }



}
