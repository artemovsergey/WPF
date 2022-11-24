using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XAML
{
    class Phone
    {

        public string Name { get; set; }
        public int Price { get; set; }
      
        public override string ToString()
        {
            return string.Format("Смартфон {0}; цена: {1}", this.Name, this.Price);
        }
    }
}
