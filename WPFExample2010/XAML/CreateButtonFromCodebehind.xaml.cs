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

namespace XAML
{
    /// <summary>
    /// Логика взаимодействия для CreateButtonFromCodebehind.xaml
    /// </summary>
    public partial class CreateButtonFromCodebehind : Window
    {
        public CreateButtonFromCodebehind()
        {
            InitializeComponent();

            Button myButton = new Button();
            myButton.Width = 100;
            myButton.Height = 30;
            myButton.Content = "Кнопка";
            layoutGrid.Children.Add(myButton);

        }
    }
}
