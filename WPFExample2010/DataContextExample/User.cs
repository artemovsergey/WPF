using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContextExample
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Company Company { get; set; }
    }
}
