using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFExample
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Button btn1 = new Button();

            btn1.Height = 100;
            btn1.Width = 200;

            sp.Children.Add(btn1);
            



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn2 = new Button();

            btn2.Height = 100;
            btn2.Width = 300;
            btn2.Margin = new Thickness(10,10,100,10);
            grid.Children.Add(btn2);


        }
    }
}
