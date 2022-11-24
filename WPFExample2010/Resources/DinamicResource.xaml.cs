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
    /// Логика взаимодействия для DinamicResource.xaml
    /// </summary>
    public partial class DinamicResource : Window
    {
        public DinamicResource()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Resources["buttonBrush"] = new SolidColorBrush(Colors.LimeGreen);
        }



        /*
        В то же время надо отметить, 
        что мы все равно может изменить статический ресурс - для этого нужно менять
        не сам объект по ключу, а его отдельные свойства
        */


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            // данное изменение будет работать и со статическими ресурсами
            SolidColorBrush buttonBrush = (SolidColorBrush)this.TryFindResource("buttonBrush");
            buttonBrush.Color = Colors.LimeGreen;
        }

    }
}
