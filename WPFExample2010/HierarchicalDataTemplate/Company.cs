using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace HierarchicalDataTemplate
{
    public class Company
    {
        public string Name { get; set; }
        public ObservableCollection<Smartphone> Phones { get; set; }
        public Company()
        {
            Phones = new ObservableCollection<Smartphone>();
        }
    }
}
