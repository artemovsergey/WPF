using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace HierarchicalDataTemplate
{

    public partial class Example : Window
    {
        public Example()
        {
            InitializeComponent();


            ObservableCollection<Company> companies = new ObservableCollection<Company>()
            {
                new Company
                {
                    Name = "Samsung",
                    Phones = new ObservableCollection<Smartphone>
                    {
                        new Smartphone {Title = "Galaxy Note 7" },
                        new Smartphone {Title = "Galaxy S 7" }
                    }
                },
                new Company
                {
                    Name = "Apple",
                    Phones = new ObservableCollection<Smartphone>
                    {
                        new Smartphone { Title="iPhone 7" },
                        new Smartphone { Title="iPhone 6S"}
                    }
                },
                new Company
                {
                    Name="Xiaomi",
                    Phones = new ObservableCollection<Smartphone>
                    {
                        new Smartphone {Title="Redmi Note 2" },
                        new Smartphone {Title="Mi5" }
                    }
                }
             };

             treeView1.ItemsSource = companies;
             mainMenu.ItemsSource = companies;
        }
    }
}
