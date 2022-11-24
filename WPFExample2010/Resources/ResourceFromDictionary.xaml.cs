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

namespace Resources
{
    /// <summary>
    /// Логика взаимодействия для ResourceFromDictionary.xaml
    /// </summary>
    public partial class ResourceFromDictionary : Window
    {
        public ResourceFromDictionary()
        {
            InitializeComponent();


            this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Dictionary.xaml") };
        }
    }
}
